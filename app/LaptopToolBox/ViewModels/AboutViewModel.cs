using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.About;
using LaptopToolBox.AppUpdater;
using LaptopToolBox.AppVersion;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public partial class AboutViewModel : ObservableObject
{
    private readonly IAboutProvider _aboutProvider = Services.ResolutionRoot.Get<IAboutProvider>();
    private readonly IAppVersionProvider _appVersionProvider = Services.ResolutionRoot.Get<IAppVersionProvider>();
    
    public IAppUpdateProvider AppUpdater { get; } = Services.ResolutionRoot.Get<IAppUpdateProvider>();

    public ObservableCollection<IAboutItem> Items => _aboutProvider.Items;

    public string Version
    {
        get
        {
            var version = _appVersionProvider.GetCurrentVersion();

            if (version == null)
            {
                return "Unknown";
            }

            return version.ToString();
        }
    }

    public void PerformUpdatesCheck()
    {
        AppUpdater.CheckForUpdate();
    }

    public void InstallUpdate()
    {
        AppUpdater.InstallUpdate();
    }
}