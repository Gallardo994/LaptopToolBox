using Ninject.Modules;

namespace GHelper.Notifications;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<INotificationService>().To<WindowsNotificationsService>().InSingletonScope();
    }
}