using GHelper.AppUpdater.Downloaders.GitHub.Models;
using GHelper.Helpers;

namespace GHelper.AppUpdater;

public interface IInstallableUpdate : IObservableObject
{
    public Release Release { get; }
    public string DownloadedFilePath { get; }
    public bool IsDownloaded { get; }
}