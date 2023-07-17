namespace LaptopToolBox.DeviceControls.Acpi;

public interface IAcpi
{
    public bool IsAvailable { get; }
    
    public bool TryDeviceSet(uint deviceId, uint status, out uint result);
    public bool TryDeviceSet(uint deviceId, byte[] buffer, out uint result);
    public bool TryDeviceGet(uint deviceId, out uint status);
    public byte[] DeviceGetBuffer(uint deviceId, uint status);
}