using Ninject.Modules;

namespace LaptopToolBox.Notifications;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<INotificationService>().To<ToastNotificationService>().InSingletonScope();
    }
}