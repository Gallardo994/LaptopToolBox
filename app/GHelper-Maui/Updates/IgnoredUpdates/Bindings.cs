using Ninject.Modules;

namespace GHelper.Updates.IgnoredUpdates;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IIgnoredUpdatesProvider>().To<IgnoredUpdatesProvider>().InSingletonScope();
    }
}