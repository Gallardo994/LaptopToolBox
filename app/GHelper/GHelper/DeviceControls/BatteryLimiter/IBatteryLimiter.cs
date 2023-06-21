namespace GHelper.DeviceControls.BatteryLimiter;

public interface IBatteryLimiter
{
    public void SetBatteryLimit(int limit);
    public int GetBatteryLimit();
    public bool IsTemporarilyUnlimited();
    public void SetTemporarilyUnlimited(bool isTemporarilyUnlimited);
    public int MinRange { get; }
    public int MaxRange { get; }
}