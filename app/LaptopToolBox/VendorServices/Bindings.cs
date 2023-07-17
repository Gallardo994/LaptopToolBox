using LaptopToolBox.VendorServices.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.VendorServices;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IVendorServicesControl>().To<AsusServicesControl>().InSingletonScope();
    }
}