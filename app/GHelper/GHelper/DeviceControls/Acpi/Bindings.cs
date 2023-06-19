using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.Acpi;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAcpi>().To<AsusAcpi>().InSingletonScope();
    }
}