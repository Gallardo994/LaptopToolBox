namespace GHelper.DeviceControls.Acpi;

public interface IAcpi
{
    public bool IsAvailable { get; }
    public int DeviceSet(uint deviceId, int status);
}