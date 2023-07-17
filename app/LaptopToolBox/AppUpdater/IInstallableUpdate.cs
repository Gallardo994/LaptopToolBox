using LaptopToolBox.AppUpdater.Downloaders.GitHub.Models;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.AppUpdater;

public interface IInstallableUpdate : IObservableObject
{
    public Release Release { get; }
    public string DownloadedFilePath { get; }
    public bool IsDownloaded { get; }
}