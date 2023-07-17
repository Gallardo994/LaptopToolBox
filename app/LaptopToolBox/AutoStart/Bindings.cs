using Ninject.Modules;

namespace LaptopToolBox.AutoStart;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAutoStartController>().To<AutoStartController>().InSingletonScope();
    }
}