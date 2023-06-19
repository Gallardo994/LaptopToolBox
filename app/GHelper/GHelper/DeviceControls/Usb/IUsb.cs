namespace GHelper.DeviceControls.Usb;

public interface IUsb
{
    public int VendorId { get; }
    public int[] DeviceIds { get; }
    public byte LightingHidId { get; }
    public byte InputHidId { get; }
}