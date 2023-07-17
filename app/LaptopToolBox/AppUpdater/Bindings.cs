using Ninject.Modules;

namespace LaptopToolBox.AppUpdater;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdateProvider>().To<AppUpdateProvider>().InSingletonScope();
    }
}