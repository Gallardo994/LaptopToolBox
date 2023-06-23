using Ninject.Modules;

namespace GHelper.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBackgroundCommandLoop>().To<BackgroundCommandLoop>().InSingletonScope();
        Bind<ISTACommandLoop>().To<STACommandLoop>().InSingletonScope();
    }
}