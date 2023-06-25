using Ninject.Modules;

namespace GHelper.IPC.Handlers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IpcBringToFrontHandler>().ToSelf().InSingletonScope();
    }
}