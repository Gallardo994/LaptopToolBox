using Ninject.Modules;

namespace LaptopToolBox.AppUpdater.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdaterCommandLoop>().To<AppUpdaterCommandLoop>().InSingletonScope();
    }
}