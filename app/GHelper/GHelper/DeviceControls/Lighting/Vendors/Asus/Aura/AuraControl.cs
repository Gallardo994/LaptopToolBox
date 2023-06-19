using System.Drawing;
using GHelper.DeviceControls.Usb;
using Ninject;

namespace GHelper.DeviceControls.Lighting.Vendors.Asus.Aura;

public class AuraControl : IAuraControl
{
    private readonly IUsb _usb;
    private readonly IHid _hid;
    private readonly IAuraCommandLoop _commandLoop;
    
    [Inject]
    public AuraControl(IUsb usb, IHid hid, IAuraCommandLoop commandLoop)
    {
        _usb = usb;
        _hid = hid;
        _commandLoop = commandLoop;
    }
    
    private byte[] CreateMessage(AuraMode mode, Color color, Color color2, AuraSpeed speed)
    {
        var msg = new byte[17];
        msg[0] = _usb.LightingHidId;
        msg[1] = 0xb3;
        msg[2] = 0x00; // Zone 
        msg[3] = (byte) mode; // Aura Mode
        msg[4] = color.R; // R
        msg[5] = color.G; // G
        msg[6] = color.B; // B
        msg[7] = (byte) speed; // aura.speed as u8;
        msg[8] = 0; // aura.direction as u8;
        msg[10] = color2.R; // R
        msg[11] = color2.G; // G
        msg[12] = color2.B; // B
        return msg;
    }

    public void Apply(AuraMode mode, Color color, Color color2, AuraSpeed speed)
    {
        var message = CreateMessage(mode, color, color2, speed);
        var command = new AuraApplyCommand(_usb, _hid, message);
        _commandLoop.Enqueue(command);
    }
}