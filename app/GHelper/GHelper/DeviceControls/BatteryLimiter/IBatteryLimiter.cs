namespace GHelper.DeviceControls.BatteryLimiter;

public interface IBatteryLimiter
{
    public void SetBatteryLimit(int limit);
    public int GetBatteryLimit();
}