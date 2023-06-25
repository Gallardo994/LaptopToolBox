using Ninject.Modules;

namespace GHelper.IPC;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IIpcResolver>().To<IpcResolver>().InSingletonScope();
    }
}