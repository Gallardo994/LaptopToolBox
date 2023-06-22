using Ninject.Modules;

namespace GHelper.Controllers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IMainWindowController>().To<MainWindowController>().InSingletonScope();
    }
}