using GHelper.Commands;
using GHelper.Configs;
using GHelper.DeviceControls.Usb;
using Ninject;

namespace GHelper.DeviceControls.Lighting.Vendors.Asus.Backlight;

public class AsusKeyboardBacklightController : IVendorKeyboardBacklightController
{
    private readonly IConfig _config;
    private readonly IBackgroundCommandLoop _commandLoop;
    private readonly IUsb _usb;
    private readonly IHid _hid;
    
    private const byte MinBrightness = 0;
    private const byte MaxBrightness = 3;
    
    [Inject]
    public AsusKeyboardBacklightController(IConfig config, IBackgroundCommandLoop commandLoop, IUsb usb, IHid hid)
    {
        _config = config;
        _commandLoop = commandLoop;
        _usb = usb;
        _hid = hid;
    }

    public void SetBrightness(byte brightness)
    {
        _commandLoop.Enqueue(new AsusKeyboardBacklightCallCommand(_usb, _hid, brightness));
    }
    
    public void IncrementBrightness()
    {
        var oldBrightness = _config.KeyboardBacklightBrightness;
        _config.KeyboardBacklightBrightness = _config.KeyboardBacklightBrightness < MaxBrightness ? (byte) (_config.KeyboardBacklightBrightness + 1) : MaxBrightness;

        if (oldBrightness == _config.KeyboardBacklightBrightness)
        {
            return;
        }
        
        SetBrightness(_config.KeyboardBacklightBrightness);
    }
    
    public void DecrementBrightness()
    {
        var oldBrightness = _config.KeyboardBacklightBrightness;
        _config.KeyboardBacklightBrightness = _config.KeyboardBacklightBrightness > MinBrightness ? (byte) (_config.KeyboardBacklightBrightness - 1) : MinBrightness;
        
        if (oldBrightness == _config.KeyboardBacklightBrightness)
        {
            return;
        }
        
        SetBrightness(_config.KeyboardBacklightBrightness);
    }
}