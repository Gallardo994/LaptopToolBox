using Ninject.Modules;

namespace GHelper.DeviceControls;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUsb>().To<AsusUsb>().InSingletonScope();
        Bind<IHid>().To<Hid>().InSingletonScope();
    }
}