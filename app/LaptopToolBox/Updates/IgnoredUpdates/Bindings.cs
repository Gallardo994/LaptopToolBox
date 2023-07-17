using Ninject.Modules;

namespace LaptopToolBox.Updates.IgnoredUpdates;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IIgnoredUpdatesProvider>().To<IgnoredUpdatesProvider>().InSingletonScope();
    }
}