using Ninject.Modules;

namespace GHelper.Tray;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ITrayProvider>().To<TrayProvider>().InSingletonScope();
    }
}