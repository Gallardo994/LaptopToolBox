using Ninject.Modules;

namespace LaptopToolBox.Updates.BackgroundWorkers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBackgroundUpdatesChecker>().To<BackgroundUpdatesChecker>().InSingletonScope();
    }
}