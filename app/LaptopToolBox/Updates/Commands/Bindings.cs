using Ninject.Modules;

namespace LaptopToolBox.Updates.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesCommandLoop>().To<UpdatesCommandLoop>().InSingletonScope();
    }
}