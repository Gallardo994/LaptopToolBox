using Ninject.Modules;

namespace GHelper.Updates.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesCommandLoop>().To<UpdatesCommandLoop>().InSingletonScope();
    }
}