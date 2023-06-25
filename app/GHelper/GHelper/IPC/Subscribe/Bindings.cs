using Ninject.Modules;

namespace GHelper.IPC.Subscribe;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IIpcBackgroundSubscriber>().To<IpcBackgroundSubscriber>().InSingletonScope();
    }
}