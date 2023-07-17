using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;
using Serilog;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusBrightnessUpKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 32;
    
    private readonly IAcpi _acpi;

    public AsusBrightnessUpKeyBind(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void Execute()
    {
        if (!_acpi.TryDeviceSet(0x00100021, (uint)AsusWmi.ASUS_WMI_BRN_UP, out _))
        {
            Log.Error("Failed to set brightness up");
        }
    }
}