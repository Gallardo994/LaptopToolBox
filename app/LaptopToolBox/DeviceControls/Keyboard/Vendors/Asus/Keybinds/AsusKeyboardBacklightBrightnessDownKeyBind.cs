using LaptopToolBox.DeviceControls.Lighting;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusKeyboardBacklightBrightnessDownKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 197;
    
    private readonly IVendorKeyboardBacklightController _vendorKeyboardBacklightController;

    public AsusKeyboardBacklightBrightnessDownKeyBind(IVendorKeyboardBacklightController vendorKeyboardBacklightController)
    {
        _vendorKeyboardBacklightController = vendorKeyboardBacklightController;
    }

    public void Execute()
    {
        _vendorKeyboardBacklightController.DecrementBrightness();
    }
}