namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceModeControl
{
    public void SetMode(IPerformanceMode performanceMode);
    public IPerformanceMode GetCurrentMode();
    public void RestoreToFallbackMode();
    public void CycleMode();
}