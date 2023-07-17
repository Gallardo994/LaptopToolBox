using LaptopToolBox.DeviceControls.Display;
using LaptopToolBox.DeviceControls.Display.NightLight;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusNightLightKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 138;
    
    private readonly IDisplayNightLightController _displayNightLightController;

    public AsusNightLightKeyBind(IDisplayNightLightController displayNightLightController)
    {
        _displayNightLightController = displayNightLightController;
    }
    
    public void Execute()
    {
        _displayNightLightController.SetNightLightState(!_displayNightLightController.IsNightLightEnabled());
    }
}