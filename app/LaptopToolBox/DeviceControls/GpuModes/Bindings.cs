using LaptopToolBox.DeviceControls.GpuModes.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.GpuModes;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IGpuModeController>().To<AsusGpuModeController>().InSingletonScope();
    }
}