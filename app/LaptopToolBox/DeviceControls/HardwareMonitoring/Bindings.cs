using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IHardwareMonitor>().To<HardwareMonitor>().InSingletonScope();
    }
}