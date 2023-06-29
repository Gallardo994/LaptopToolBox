using Ninject.Modules;

namespace GHelper.DeviceControls.HardwareMonitoring;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IHardwareMonitor>().To<HardwareMonitor>().InSingletonScope();
    }
}