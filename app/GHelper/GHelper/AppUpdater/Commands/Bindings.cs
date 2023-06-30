using Ninject.Modules;

namespace GHelper.AppUpdater.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdaterCommandLoop>().To<AppUpdaterCommandLoop>().InSingletonScope();
    }
}