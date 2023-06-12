using Ninject.Modules;

namespace GHelper.Core;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ICoreRunner>().To<MainCoreRunner>().InSingletonScope();
    }
}