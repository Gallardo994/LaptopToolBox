using Ninject.Modules;

namespace LaptopToolBox.AppUpdater.BackgroundWorkers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBackgroundAppUpdateChecker>().To<BackgroundAppUpdateChecker>().InSingletonScope();
    }
}