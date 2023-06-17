namespace GHelper.DeviceControls;

public interface IUsb
{
    public int AsusId { get; }
    public int[] DeviceIds { get; }
    public byte AuraHidId { get; }
    public byte[] MessageSet { get; }
    public byte[] MessageApply { get; }
}