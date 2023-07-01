using Ninject.Modules;

namespace GHelper.Updates.Core;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesUrlProvider>().To<UpdatesUrlProvider>().InSingletonScope();
        Bind<IUpdatesChecker>().To<UpdatesChecker>().InSingletonScope();
        Bind<IUpdatesProvider>().To<UpdatesProvider>().InSingletonScope();
    }
}