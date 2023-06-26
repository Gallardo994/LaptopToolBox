namespace GHelper.DeviceControls.Display.Screens;

public struct GdiDeviceId
{
    public string LpszDeviceName;
    
    public override string ToString()
    {
        return $"{nameof(LpszDeviceName)}={LpszDeviceName}";
    }
}