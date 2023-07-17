using Ninject.Modules;

namespace LaptopToolBox.IPC;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IIpcResolver>().To<IpcResolver>().InSingletonScope();
    }
}