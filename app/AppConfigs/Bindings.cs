using Ninject.Modules;

namespace GHelper.AppConfigs;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppConfigPathProvider>().To<AppConfigPathProvider>().InSingletonScope();
        Bind<IAppConfig>().To<LocalAppConfig>().InSingletonScope();
    }
}