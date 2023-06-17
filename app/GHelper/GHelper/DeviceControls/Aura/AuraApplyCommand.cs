using GHelper.Commands;

namespace GHelper.DeviceControls.Aura;

public class AuraApplyCommand : ICommand
{
    private readonly IUsb _usb;
    private readonly IHid _hid;
    private readonly byte[] _message;
    
    public AuraApplyCommand(IUsb usb, IHid hid, byte[] message)
    {
        _usb = usb;
        _hid = hid;
        _message = message;
    }

    public void Execute()
    {
        var devices = _hid.GetHidDevicesBlocking(_usb.DeviceIds);
            
        foreach (var device in devices)
        {
            device.OpenDevice();
            
            device.WriteFeatureData(_message);
            device.WriteFeatureData(_usb.MessageSet);
            device.WriteFeatureData(_usb.MessageApply);
            
            device.CloseDevice();
        }
    }
}