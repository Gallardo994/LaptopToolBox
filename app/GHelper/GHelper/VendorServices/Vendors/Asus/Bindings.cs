using Ninject.Modules;

namespace GHelper.VendorServices.Vendors.Asus;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAsusServicesControlCommandLoop>().To<AsusServicesControlCommandLoop>().InSingletonScope();
    }
}