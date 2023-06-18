using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusVendorKeyboardHandler : IVendorKeyboardHandler
{
    private readonly IVendorKeyboardListener _vendorKeyboardListener;
    
    [Inject]
    public AsusVendorKeyboardHandler(IVendorKeyboardListener vendorKeyboardListener)
    {
        _vendorKeyboardListener = vendorKeyboardListener;
        
        _vendorKeyboardListener.KeyHandler += KeyHandler;
    }

    private void KeyHandler(int keyCode)
    {
        Log.Debug("Key {KeyCode} pressed", keyCode);
    }
    
    public void Dispose()
    {
        _vendorKeyboardListener.KeyHandler -= KeyHandler;
    }
}