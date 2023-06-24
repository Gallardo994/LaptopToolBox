namespace GHelper.DeviceControls.Display;

public interface IDisplayNightLightController
{
    public void SetNightLightState(bool state);
    public bool IsNightLightEnabled();
}