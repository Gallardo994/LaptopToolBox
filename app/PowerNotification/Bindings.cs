using Ninject.Modules;

namespace GHelper.PowerNotification;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IPowerNotifier>().To<PowerNotifier>().InSingletonScope();
    }
}