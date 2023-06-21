using Ninject.Modules;

namespace GHelper.About;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAboutProvider>().To<AboutProvider>().InSingletonScope();
    }
}