using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.About;
using GHelper.AppUpdater;
using GHelper.AppVersion;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

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