using Ninject.Modules;

namespace LaptopToolBox.AppUpdater.NavIntegration;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdateNotificationBellUpdater>().To<AppUpdateNotificationBellUpdater>().InSingletonScope();
    }
}