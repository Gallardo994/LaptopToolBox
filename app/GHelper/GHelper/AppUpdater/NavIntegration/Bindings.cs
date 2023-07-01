using Ninject.Modules;

namespace GHelper.AppUpdater.NavIntegration;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdateNotificationBellUpdater>().To<AppUpdateNotificationBellUpdater>().InSingletonScope();
    }
}