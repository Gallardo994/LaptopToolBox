using LaptopToolBox.DeviceControls.PerformanceModes.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.PerformanceModes;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IPerformanceModesProvider>().To<AsusPerformanceModesProvider>().InSingletonScope();
        Bind<IPerformanceModeControl>().To<AsusPerformanceModeControl>().InSingletonScope();
    }
}