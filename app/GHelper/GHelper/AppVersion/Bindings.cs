using Ninject.Modules;

namespace GHelper.AppVersion;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppVersionProvider>().To<AppVersionProvider>().InSingletonScope();
    }
}