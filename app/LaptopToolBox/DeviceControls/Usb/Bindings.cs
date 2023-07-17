using LaptopToolBox.DeviceControls.Usb.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Usb;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUsb>().To<AsusUsb>().InSingletonScope();
    }
}