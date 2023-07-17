using Ninject.Modules;

namespace LaptopToolBox.AppVersion;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppVersionProvider>().To<AppVersionProvider>().InSingletonScope();
    }
}