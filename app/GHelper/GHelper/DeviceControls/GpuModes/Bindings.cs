using GHelper.DeviceControls.GpuModes.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.GpuModes;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IGpuModeController>().To<AsusGpuModeController>().InSingletonScope();
    }
}