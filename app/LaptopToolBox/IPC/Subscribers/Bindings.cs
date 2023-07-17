using Ninject.Modules;

namespace LaptopToolBox.IPC.Subscribers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IIpcBackgroundSubscriber>().To<IpcBackgroundSubscriber>().InSingletonScope();
    }
}