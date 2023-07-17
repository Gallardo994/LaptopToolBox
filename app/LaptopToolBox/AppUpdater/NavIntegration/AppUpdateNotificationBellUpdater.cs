using System.ComponentModel;
using LaptopToolBox.Updates.Core;
using LaptopToolBox.Pages;
using Ninject;

namespace LaptopToolBox.AppUpdater.NavIntegration;

public class AppUpdateNotificationBellUpdater : IAppUpdateNotificationBellUpdater
{
    private readonly IPageProvider _pageProvider;
    private readonly IAppUpdateProvider _appUpdateProvider;
    
    [Inject]
    public AppUpdateNotificationBellUpdater(IPageProvider pageProvider, IAppUpdateProvider appUpdateProvider)
    {
        _pageProvider = pageProvider;
        _appUpdateProvider = appUpdateProvider;
    }

    public void Start()
    {
        RefreshNotificationBell();
        _appUpdateProvider.PropertyChanged += UpdatesProviderOnPropertyChanged;
    }
    
    public void Stop()
    {
        _appUpdateProvider.PropertyChanged -= UpdatesProviderOnPropertyChanged;
    }
    
    private void UpdatesProviderOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IAppUpdateProvider.IsUpdateInstallAvailable))
        {
            RefreshNotificationBell();
        }
    }

    private FlyoutPageItem GetTargetPage()
    {
        return _pageProvider.GetPageItem<AboutPage>();
    }
    
    private void RefreshNotificationBell()
    {
        GetTargetPage().NotificationCount = _appUpdateProvider.IsUpdateInstallAvailable ? 1 : 0;
    }
}