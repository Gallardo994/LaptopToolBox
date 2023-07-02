namespace GHelper.DeviceControls.Acpi;

public interface IAcpi
{
    public bool IsAvailable { get; }
    
    public void Initialize();
    
    public uint DeviceSet(uint deviceId, uint status);
    public uint DeviceSet(uint deviceId, byte[] buffer);
    public byte[] DeviceSetWithBuffer(uint deviceId, uint status);
    
    public uint DeviceGet(uint deviceId);
    public bool TryDeviceGet(uint deviceId, out uint status);
    public byte[] DeviceGetWithBuffer(uint deviceId);
    public byte[] DeviceGetWithBuffer(uint deviceId, uint status);
}