using GHelper.AppUpdater.NavIntegration;
using GHelper.Updates.NavIntegration;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

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