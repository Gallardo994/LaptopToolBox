using GHelper.DeviceControls.GPUs.Vendors.Nvidia;
using Ninject.Modules;

namespace GHelper.DeviceControls.GPUs;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IGpuGeneralInfoProvider>().To<WmiGpuGeneralInfoProvider>().InSingletonScope();
        
        var nvidiaGpu = new NvidiaGpu();
        
        if (nvidiaGpu.IsAvailable())
        {
            Bind<IGpuControl>().ToConstant(nvidiaGpu);
        }
    }
}