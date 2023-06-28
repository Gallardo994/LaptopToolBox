using Ninject.Modules;

namespace GHelper.DeviceControls.GPUs;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IGpuGeneralInfoProvider>().To<WmiGpuGeneralInfoProvider>().InSingletonScope();
        Bind<IGpuControl>().To<MultiplexedGpuControl>().InSingletonScope();
    }
}