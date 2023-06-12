using Ninject.Modules;

namespace GHelper.Execute;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IShellRunner>().To<ShellRunner>().InSingletonScope();
    }
}