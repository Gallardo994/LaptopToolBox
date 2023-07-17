using Ninject.Modules;

namespace LaptopToolBox.Updates.LocalDriversVersion;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ILocalDriversVersionProvider>().To<LocalDriversVersionProvider>().InSingletonScope();
    }
}