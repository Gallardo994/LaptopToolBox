using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.AppUpdater.Downloaders.GitHub.Models;

namespace LaptopToolBox.AppUpdater;

public partial class InstallableUpdate : ObservableObject, IInstallableUpdate
{
    [ObservableProperty] private Release _release;
    [ObservableProperty] private string _downloadedFilePath;
    
    public bool IsDownloaded => !string.IsNullOrEmpty(DownloadedFilePath);
    
    public InstallableUpdate(Release release, string downloadedFilePath)
    {
        _release = release;
        _downloadedFilePath = downloadedFilePath;
    }
}