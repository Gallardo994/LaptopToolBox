using Ninject.Modules;

namespace GHelper.DeviceControls.PerformanceModes;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IPerformanceModesProvider>().To<PerformanceModesProvider>().InSingletonScope();
        Bind<IPerformanceModeControl>().To<PerformanceModeControl>().InSingletonScope();
    }
}