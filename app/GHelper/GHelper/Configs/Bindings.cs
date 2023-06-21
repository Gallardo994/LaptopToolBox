using Ninject.Modules;

namespace GHelper.Configs;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IConfig>().To<Config>().InSingletonScope();
    }
}