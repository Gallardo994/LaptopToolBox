namespace GHelper.DeviceControls.Fans;

public interface IFanController
{
    public int GetCpuFanRpm();
    public int GetGpuFanRpm();
}