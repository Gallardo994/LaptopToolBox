using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusBrightnessDownKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 16;
    
    private readonly IAcpi _acpi;

    public AsusBrightnessDownKeyBind(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void Execute()
    {
        _acpi.TryDeviceSet(0x00100021, (uint) AsusWmi.ASUS_WMI_BRN_DOWN, out _);
    }
}