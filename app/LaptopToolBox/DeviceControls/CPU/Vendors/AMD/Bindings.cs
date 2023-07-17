using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IOls>().To<Ols>().InSingletonScope();
        Bind<IAmdAddressesProvider>().To<AmdAddressesProvider>().InSingletonScope();
        Bind<IRyzenAccess>().To<RyzenAccess>().InSingletonScope();
        Bind<IRyzenProxy>().To<RyzenProxy>().InSingletonScope();
    }
}