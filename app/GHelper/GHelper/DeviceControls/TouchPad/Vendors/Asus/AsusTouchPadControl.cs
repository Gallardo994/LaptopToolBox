using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Usb;
using Ninject;

namespace GHelper.DeviceControls.TouchPad.Vendors.Asus;

public class AsusTouchPadControl : ITouchPadControl
{
    private readonly IUsb _usb;
    private readonly IHid _hid;

    private readonly byte[] _message;
    
    [Inject]
    public AsusTouchPadControl(IUsb usb, IHid hid)
    {
        _usb = usb;
        _hid = hid;

        _message = new byte[] { _usb.InputHidId, 0xf4, 0x6b };
    }

    public void Toggle()
    {
        var input = _hid.GetDevice(_usb.VendorId, _usb.DeviceIds, _usb.InputHidId);
        
        if (input is not { IsConnected: true })
        {
            return;
        }
        
        input.WriteFeatureData(_message);
    }
}