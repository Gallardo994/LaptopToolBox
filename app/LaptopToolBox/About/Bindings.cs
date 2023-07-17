using Ninject.Modules;

namespace LaptopToolBox.About;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAboutProvider>().To<AboutProvider>().InSingletonScope();
    }
}