namespace GHelper.DeviceControls.Acpi;

public interface IAcpi
{
    public bool IsAvailable { get; }
    
    public int DeviceSet(uint deviceId, int status);
    public byte[] DeviceSetWithBuffer(uint deviceId, int status);
    
    public int DeviceGet(uint deviceId);
    public byte[] DeviceGetWithBuffer(uint deviceId);
}