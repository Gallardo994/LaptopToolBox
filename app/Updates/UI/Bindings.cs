using Ninject.Modules;

namespace GHelper.Updates.UI;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesWindow>().To<UpdatesWindow>().InSingletonScope();
    }
}