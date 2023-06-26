
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

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
        _acpi.DeviceSet(0x00100021, (uint) AsusWmi.ASUS_WMI_BRN_UP);
    }
}