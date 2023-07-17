using Ninject.Modules;

namespace LaptopToolBox.AutoEco;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAutoEco>().To<AutoEco>().InSingletonScope();
    }
}