using LaptopToolBox.DeviceControls.CPU.Vendors.AMD;
using LaptopToolBox.DeviceControls.CPU.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.CPU;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ICpuGeneralInfoProvider>().To<WmiCpuGeneralInfoProvider>().InSingletonScope();
        Bind<ICpuFamilyProvider>().To<AmdCpuFamilyProvider>().InSingletonScope();
        Bind<ICpuDirectControl>().To<AmdCpuDirectControl>().InSingletonScope();
        Bind<ICpuControl>().To<AsusCpu>().InSingletonScope();
    }
}