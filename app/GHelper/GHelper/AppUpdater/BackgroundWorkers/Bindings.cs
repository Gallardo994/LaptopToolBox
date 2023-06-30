using Ninject.Modules;

namespace GHelper.AppUpdater.BackgroundWorkers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBackgroundAppUpdateChecker>().To<BackgroundAppUpdateChecker>().InSingletonScope();
    }
}