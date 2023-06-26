namespace GHelper.DeviceControls.Acpi;

public interface IAcpi
{
    public bool IsAvailable { get; }
    
    public void Initialize();
    
    public uint DeviceSet(uint deviceId, uint status);
    public byte[] DeviceSetWithBuffer(uint deviceId, uint status);
    
    public uint DeviceGet(uint deviceId);
    public byte[] DeviceGetWithBuffer(uint deviceId);
}