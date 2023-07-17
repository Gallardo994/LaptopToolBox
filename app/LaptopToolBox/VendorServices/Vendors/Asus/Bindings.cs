using Ninject.Modules;

namespace LaptopToolBox.VendorServices.Vendors.Asus;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAsusServicesControlCommandLoop>().To<AsusServicesControlCommandLoop>().InSingletonScope();
    }
}