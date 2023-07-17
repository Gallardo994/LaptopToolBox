using LaptopToolBox.Commands;
using LaptopToolBox.DeviceControls.Usb;
using Serilog;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Backlight;

public class AsusKeyboardBacklightCallCommand : IBackgroundCommand
{
    private readonly IUsb _usb;
    private readonly IHid _hid;
    private readonly byte _brightness;
    
    public AsusKeyboardBacklightCallCommand(IUsb usb, IHid hid, byte brightness)
    {
        _usb = usb;
        _hid = hid;
        _brightness = brightness;
    }
    
    public void Execute()
    {
        Log.Debug("Setting Asus Keyboard Backlight to {Brightness}", _brightness);
        
        var devices = _hid.GetHidDevicesBlocking(_usb.VendorId, _usb.DeviceIds, 0);
        
        foreach (var device in devices)
        {
            device.OpenDevice();

            if (device.ReadFeatureData(out var buffer, _usb.LightingHidId))
            {
                buffer[4] = _brightness;
                device.WriteFeatureData(buffer);
            }

            device.CloseDevice();
        }
    }
}