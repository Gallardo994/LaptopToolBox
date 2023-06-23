using GHelper.DeviceControls.PerformanceModes;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusPerformanceModeKeyBind : IVendorKeyBind
{
    private readonly IPerformanceModeControl _performanceModeControl;
    
    public int Key { get; set; } = 174;
    

    public AsusPerformanceModeKeyBind(IPerformanceModeControl performanceModeControl)
    {
        _performanceModeControl = performanceModeControl;
    }

    public void Execute()
    {
        _performanceModeControl.CycleMode();
    }
}