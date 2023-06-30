using GHelper.Initializers.ConcreteInitializers;
using Ninject.Modules;

namespace GHelper.Initializers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<BatteryInitializer>().ToSelf().InSingletonScope();
        Bind<VendorKeyRegisterInitializer>().ToSelf().InSingletonScope();
        Bind<CpuControlInitializer>().ToSelf().InSingletonScope();
        Bind<PerformanceModeInitializer>().ToSelf().InSingletonScope();
        Bind<KeyboardBacklightInitializer>().ToSelf().InSingletonScope();
        Bind<AutoOverdriveInitializer>().ToSelf().InSingletonScope();
        Bind<HardwareMonitorInitializer>().ToSelf().InSingletonScope();
        Bind<BackgroundAppUpdateInitializer>().ToSelf().InSingletonScope();
        
        Bind<MainWindowInitializer>().ToSelf().InSingletonScope();
        
        Bind<IInitializersProvider>().To<InitializersProvider>().InSingletonScope();
    }
}