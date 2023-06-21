using GHelper.DeviceControls.CPU.Vendors.AMD;
using GHelper.DeviceControls.CPU.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.CPU;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ICpuDirectControl>().To<AmdCpuDirectControl>().InSingletonScope();
        Bind<ICpuControl>().To<AsusCpu>().InSingletonScope();
    }
}