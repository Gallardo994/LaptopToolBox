using GHelper.DeviceControls.BatteryLimiter.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.BatteryLimiter;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBatteryLimiter>().To<AsusBatteryLimiter>().InSingletonScope();
    }
}