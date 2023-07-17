using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAuraCommandLoop>().To<AuraCommandLoop>().InSingletonScope();

        Bind<IAuraControl>().To<AuraControl>().InSingletonScope();
        Bind<IAuraModesProvider>().To<AuraModesProvider>().InSingletonScope();
        Bind<IAuraSpeedsProvider>().To<AuraSpeedsProvider>().InSingletonScope();
    }
}