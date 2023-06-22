using Ninject.Modules;

namespace GHelper.AutoStart;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAutoStartController>().To<AutoStartController>().InSingletonScope();
    }
}