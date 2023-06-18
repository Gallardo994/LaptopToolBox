namespace GHelper.DeviceControls;

public interface IAcpi
{
    public int DeviceSet(uint deviceId, int status, string logName);
}