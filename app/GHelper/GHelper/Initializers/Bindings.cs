using Ninject.Modules;

namespace GHelper.Initializers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IInitializersProvider>().To<InitializersProvider>().InSingletonScope();
    }
}