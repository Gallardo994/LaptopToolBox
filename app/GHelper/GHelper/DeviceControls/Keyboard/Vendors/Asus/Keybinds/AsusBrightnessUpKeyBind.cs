
using GHelper.DeviceControls.Acpi;

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
        _acpi.DeviceSet(0x00100021, 0x20);
    }
}