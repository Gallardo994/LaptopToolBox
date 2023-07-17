using Ninject.Modules;

namespace LaptopToolBox.ModelInfo;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IModelInfoProvider>().To<ModelInfoProvider>().InSingletonScope();
    }
}