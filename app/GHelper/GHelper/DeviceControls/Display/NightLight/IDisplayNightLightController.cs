namespace GHelper.DeviceControls.Display.NightLight;

public interface IDisplayNightLightController
{
    public void SetNightLightState(bool state);
    public bool IsNightLightEnabled();
}