using Ninject.Modules;

namespace LaptopToolBox.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBackgroundCommandLoop>().To<GeneralBackgroundCommandLoop>().InSingletonScope();
        Bind<ISTACommandLoop>().To<STACommandLoop>().InSingletonScope();
    }
}