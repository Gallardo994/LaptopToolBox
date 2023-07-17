using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.PowerLimits.Vendors.Asus;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAsusPowerLimitFactory>().To<AsusPowerLimitFactory>().InSingletonScope();
    }
}