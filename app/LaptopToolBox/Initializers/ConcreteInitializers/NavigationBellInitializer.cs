using LaptopToolBox.AppUpdater.NavIntegration;
using LaptopToolBox.Updates.NavIntegration;
using Ninject;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

public class NavigationBellInitializer : IInitializer
{
    private readonly IUpdatesNotificationBellUpdater _updatesNotificationBellUpdater;
    private readonly IAppUpdateNotificationBellUpdater _appUpdateNotificationBellUpdater;
    
    [Inject]
    public NavigationBellInitializer(IUpdatesNotificationBellUpdater updatesNotificationBellUpdater, IAppUpdateNotificationBellUpdater appUpdateNotificationBellUpdater)
    {
        _updatesNotificationBellUpdater = updatesNotificationBellUpdater;
        _appUpdateNotificationBellUpdater = appUpdateNotificationBellUpdater;
    }
    
    public void Initialize()
    {
        _updatesNotificationBellUpdater.Start();
        _appUpdateNotificationBellUpdater.Start();
    }
}