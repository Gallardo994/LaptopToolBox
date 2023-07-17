using LaptopToolBox.DeviceControls.Keyboard.Vendors;
using LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Keyboard;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IVendorKeyboardListener>().To<AsusKeyboardListener>().InSingletonScope();
        Bind<IVendorKeyboardHandler>().To<AsusVendorKeyboardHandler>().InSingletonScope();
        
        Bind<IVendorKeyRegister>().To<AsusKeyRegister>().InSingletonScope();
    }
}