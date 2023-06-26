using GHelper.DeviceControls.TouchPad;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusToggleTouchpadKeyBind : IVendorKeyBind
{
    private readonly ITouchPadControl _touchPadControl;
    
    public int Key { get; } = 107;
    
    public AsusToggleTouchpadKeyBind(ITouchPadControl touchPadControl)
    {
        _touchPadControl = touchPadControl;
    }
    
    public void Execute()
    {
        _touchPadControl.Toggle();
    }
}