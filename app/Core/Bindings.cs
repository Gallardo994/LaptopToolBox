using Ninject.Modules;

namespace GHelper.Core;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IScheduler>().To<Scheduler>().InSingletonScope();
        Bind<ICoreRunner>().To<MainCoreRunner>().InSingletonScope();
    }
}