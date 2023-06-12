using Ninject.Modules;

namespace GHelper.Updates;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IModelInfoProvider>().To<ModelInfoProvider>().InSingletonScope();
        Bind<IUpdatesUrlProvider>().To<UpdatesUrlProvider>().InSingletonScope();
    }
}