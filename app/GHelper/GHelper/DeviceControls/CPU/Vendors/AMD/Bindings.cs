using Ninject.Modules;

namespace GHelper.DeviceControls.CPU.Vendors.AMD;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IOls>().To<Ols>().InSingletonScope();
        Bind<IAmdFamilyProvider>().To<AmdFamilyProvider>().InSingletonScope();
        Bind<IAmdAddressesProvider>().To<AmdAddressesProvider>().InSingletonScope();
        Bind<IRyzenAccess>().To<RyzenAccess>().InSingletonScope();
        Bind<IRyzenProxy>().To<RyzenProxy>().InSingletonScope();
    }
}