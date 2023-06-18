namespace GHelper.DeviceControls;

public interface IUsb
{
    public int VendorId { get; }
    public int[] DeviceIds { get; }
    public byte LightingHidId { get; }
    public byte InputHidId { get; }
    public byte[] MessageSet { get; }
    public byte[] MessageApply { get; }
}