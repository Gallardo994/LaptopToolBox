using Ninject.Modules;

namespace GHelper.AutoOverdrive;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAutoOverdrive>().To<AutoOverdrive>().InSingletonScope();
    }
}