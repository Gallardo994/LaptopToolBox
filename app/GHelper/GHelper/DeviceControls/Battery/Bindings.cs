using GHelper.DeviceControls.Battery.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.Battery;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBattery>().To<AsusBattery>().InSingletonScope();
        Bind<IBatteryStateProvider>().To<WindowsBatteryStateProvider>().InSingletonScope();
    }
}