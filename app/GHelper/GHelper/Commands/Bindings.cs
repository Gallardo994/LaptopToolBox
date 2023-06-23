using Ninject.Modules;

namespace GHelper.Commands;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IThreadCommandLoop>().To<GlobalCommandLoop>().InSingletonScope();
    }
}