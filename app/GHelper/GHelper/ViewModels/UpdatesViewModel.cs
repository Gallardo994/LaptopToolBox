using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Injection;
using GHelper.Updates.Core;
using GHelper.Updates.Models;
using GHelper.Web;
using Ninject;

namespace GHelper.ViewModels;

public class UpdatesViewModel : ObservableObject
{
    public readonly IUpdatesProvider UpdatesProvider = Services.ResolutionRoot.Get<IUpdatesProvider>();
    
    private readonly IHttpClientFactory _httpClientFactory = Services.ResolutionRoot.Get<IHttpClientFactory>();

    public async Task RequestDownloadUpdate(IUpdate update)
    {
        await Launcher.LaunchUriAsync(new Uri(update.DownloadUrl));
    }

    private async Task<string> DownloadFile(Uri uri)
    {
        var tempFilePath = string.Concat(Path.GetTempFileName(), uri.AbsolutePath.AsSpan(uri.AbsolutePath.LastIndexOf('.')));
        
        using var httpClient = _httpClientFactory.Get();
        await httpClient.DownloadFileAsync(uri, tempFilePath);

        return tempFilePath;
    }
    
    public async Task RequestDownloadAndInstallUpdate(Uri uri)
    {
        var tempFilePath = await DownloadFile(uri);
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = tempFilePath,
                UseShellExecute = true,
            }
        };
        
        process.Start();
    }

    public void CheckForUpdates()
    {
        UpdatesProvider.CheckForUpdates();
    }
}