namespace GHelper.DeviceControls.Display.RefreshRate;

public interface IRefreshRateController
{
    public void SetMode(RefreshRateMode mode);
    public RefreshRateMode GetMode();
}