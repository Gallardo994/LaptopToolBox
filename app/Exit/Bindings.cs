using Ninject.Modules;

namespace GHelper.Exit;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IExitProvider>().To<ExitProvider>().InSingletonScope();
    }
}