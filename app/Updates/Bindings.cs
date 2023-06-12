using Ninject.Modules;

namespace GHelper.Updates;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IModelInfoProvider>().To<ModelInfoProvider>().InSingletonScope();
        Bind<IUpdatesUrlProvider>().To<UpdatesUrlProvider>().InSingletonScope();
        Bind<IIgnoredUpdatesProvider>().To<IgnoredUpdatesProvider>().InSingletonScope();
        Bind<ILocalDriversVersionProvider>().To<LocalDriversVersionProvider>().InSingletonScope();
        Bind<IUpdatesChecker>().To<UpdatesChecker>().InSingletonScope();
    }
}