using GHelper.DeviceControls.Keyboard.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.Keyboard.Vendors;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IVendorKeyboardHandler>().To<AsusVendorKeyboardHandler>();
    }
}