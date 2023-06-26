namespace GHelper.DeviceControls.Display.RefreshRate.Vendors.Asus;

public interface IOverdriveController
{
    public void SetState(bool state);
    public bool GetState();
}