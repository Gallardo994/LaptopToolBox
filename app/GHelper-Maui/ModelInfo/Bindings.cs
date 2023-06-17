using Ninject.Modules;

namespace GHelper.ModelInfo;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IModelInfoProvider>().To<ModelInfoProvider>().InSingletonScope();
    }
}