using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Acpi;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAcpi>().To<AsusAcpi>().InSingletonScope();
    }
}