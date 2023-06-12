using Ninject.Modules;

namespace GHelper.Powerline;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IPowerlineStatusProvider>().To<PowerlineStatusProvider>().InSingletonScope();
    }
}