using LaptopToolBox.DeviceControls.Lighting;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusKeyboardBacklightBrightnessUpKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 196;
    
    private readonly IVendorKeyboardBacklightController _vendorKeyboardBacklightController;

    public AsusKeyboardBacklightBrightnessUpKeyBind(IVendorKeyboardBacklightController vendorKeyboardBacklightController)
    {
        _vendorKeyboardBacklightController = vendorKeyboardBacklightController;
    }

    public void Execute()
    {
        _vendorKeyboardBacklightController.IncrementBrightness();
    }
}