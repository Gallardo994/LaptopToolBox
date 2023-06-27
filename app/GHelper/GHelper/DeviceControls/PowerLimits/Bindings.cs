using GHelper.DeviceControls.PowerLimits.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.PowerLimits;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IPowerLimitController>().To<AsusPowerLimitController>().InSingletonScope();
    }
}