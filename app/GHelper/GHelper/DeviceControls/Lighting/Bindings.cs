using GHelper.DeviceControls.Lighting.Vendors.Asus.Backlight;
using Ninject.Modules;

namespace GHelper.DeviceControls.Lighting;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IVendorKeyboardBacklightController>().To<AsusKeyboardBacklightController>();
    }
}