using GHelper.VendorServices.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.VendorServices;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IVendorServicesControl>().To<AsusServicesControl>().InSingletonScope();
    }
}