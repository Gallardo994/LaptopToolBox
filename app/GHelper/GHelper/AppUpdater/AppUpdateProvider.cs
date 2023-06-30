using System;
using System.Diagnostics;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.AppUpdater.Commands;
using GHelper.AppUpdater.Downloaders;
using GHelper.AppUpdater.Downloaders.GitHub.Models;
using GHelper.Commands;
using GHelper.Helpers;
using GHelper.Notifications;
using Ninject;
using Serilog;

namespace GHelper.AppUpdater;

public partial class AppUpdateProvider : ObservableObject, IAppUpdateProvider
{
    private readonly IAppUpdateDownloader _appUpdateDownloader;
    private readonly IAppUpdaterCommandLoop _appUpdaterCommandLoop;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly INotificationService _notificationService;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdateBusy))]
    private bool _isCheckingForUpdates;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdateBusy))]
    private bool _isDownloadingUpdate;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdateBusy))]
    [NotifyPropertyChangedFor(nameof(IsUpdateInstallAvailable))]
    private IInstallableUpdate _installableUpdate;
    public bool IsUpdateInstallAvailable => InstallableUpdate != null && InstallableUpdate.IsDownloaded;
    
    public bool IsUpdateBusy => IsCheckingForUpdates || IsDownloadingUpdate || IsUpdateInstallAvailable;

    [Inject]
    public AppUpdateProvider(IAppUpdateDownloader appUpdateDownloader, IAppUpdaterCommandLoop appUpdaterCommandLoop, ISTACommandLoop staCommandLoop, INotificationService notificationService)
    {
        _appUpdateDownloader = appUpdateDownloader;
        _appUpdaterCommandLoop = appUpdaterCommandLoop;
        _staCommandLoop = staCommandLoop;
        _notificationService = notificationService;
    }

    public void CheckForUpdate()
    {
        if (IsCheckingForUpdates)
        {
            Log.Information("Update checking is already in progress");
            return;
        }
        
        IsCheckingForUpdates = true;

        _appUpdaterCommandLoop.Enqueue(() =>
        {
            var releaseTask = _appUpdateDownloader.GetSuggestedUpdate();
            releaseTask.Wait();

            _staCommandLoop.Enqueue(() =>
            {
                IsCheckingForUpdates = false;
                
                if (releaseTask.Result == null)
                {
                    Log.Information("Update check failed");
                    return;
                }
                
                _notificationService.Show(NotificationCategory.AppUpdateStartedDownloading, $"New update is downloading", $"New version: {releaseTask.Result.Name}");
                DownloadUpdate(releaseTask.Result);
            });
        });
    }
    
    public void DownloadUpdate(Release release)
    {
        if (IsDownloadingUpdate)
        {
            Log.Information("Update downloading is already in progress");
            return;
        }

        IsDownloadingUpdate = true;

        _appUpdaterCommandLoop.Enqueue(() =>
        {
            var downloadTask = _appUpdateDownloader.Download(release, CancellationToken.None);
            downloadTask.Wait();

            _staCommandLoop.Enqueue(() =>
            {
                IsDownloadingUpdate = false;
                
                if (string.IsNullOrEmpty(downloadTask.Result))
                {
                    Log.Information("Update download failed");
                    return;
                }
                
                InstallableUpdate = new InstallableUpdate(release, downloadTask.Result);

                _notificationService.Show(NotificationCategory.AppUpdateAvailable, $"New update is available", $"New version: {release.Name}");
            });
        });
    }

    public void InstallUpdate()
    {
        if (!IsUpdateInstallAvailable)
        {
            Log.Information("Update is not available for installation");
            return;
        }

#if DEBUG
        Log.Debug("Trying to install update {version}, downloaded to {path}", InstallableUpdate.Release.Name, InstallableUpdate.DownloadedFilePath);
#else
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "UpdateInstaller\\UpdateInstaller.exe",
            Arguments =
                $"--zipPath=\"{InstallableUpdate.DownloadedFilePath}\" --targetDir=\"{AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')}\" --runAfter=\"GHelper.exe\"",
            UseShellExecute = true,
        };

        Process.Start(processStartInfo);

        ApplicationHelper.Exit();
#endif
    }
}