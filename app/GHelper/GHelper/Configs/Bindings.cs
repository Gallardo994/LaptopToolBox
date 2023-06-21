using Ninject.Modules;

namespace GHelper.Configs;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IConfigSaveCommandLoop>().To<ConfigSaveCommandLoop>().InSingletonScope();
        Bind<IConfig>().To<Config>().InSingletonScope();
    }
}