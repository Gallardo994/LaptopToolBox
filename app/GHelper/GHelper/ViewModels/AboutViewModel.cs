using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.About;
using GHelper.AppUpdater;
using GHelper.Injection;
using GHelper.Updates.Core;
using Ninject;

namespace GHelper.ViewModels;

public class AboutViewModel : ObservableObject
{
    private readonly IAboutProvider _aboutProvider = Services.ResolutionRoot.Get<IAboutProvider>();
    private readonly IAppUpdater _appUpdater = Services.ResolutionRoot.Get<IAppUpdater>();

    public ObservableCollection<IAboutItem> Items => _aboutProvider.Items;
    public string Version
    {
        get
        {
            var version = _appUpdater.GetCurrentVersion();
            
            if (version == null)
            {
                return "Unknown";
            }
            
            return version.ToString();
        }
    }
}