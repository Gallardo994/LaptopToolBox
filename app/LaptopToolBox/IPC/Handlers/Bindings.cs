using Ninject.Modules;

namespace LaptopToolBox.IPC.Handlers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IpcBringToFrontHandler>().ToSelf().InSingletonScope();
    }
}