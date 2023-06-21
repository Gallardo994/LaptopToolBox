using GHelper.DeviceControls.Acpi;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

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
        _acpi.DeviceSet(0x00100021, 0x10);
    }
}