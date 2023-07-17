using Ninject.Modules;

namespace LaptopToolBox.Updates.NavIntegration;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesNotificationBellUpdater>().To<UpdatesNotificationBellUpdater>().InSingletonScope();
    }
}