using Ninject.Modules;

namespace GHelper.Updates.BackgroundWorkers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBackgroundUpdatesChecker>().To<BackgroundUpdatesChecker>().InSingletonScope();
    }
}