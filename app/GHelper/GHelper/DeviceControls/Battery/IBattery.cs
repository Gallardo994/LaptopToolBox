namespace GHelper.DeviceControls.Battery;

public interface IBattery
{
    public bool IsBatteryLimitSupported { get; }
    public void SetBatteryLimit(int limit);
    public int GetBatteryLimit();
    public bool IsTemporarilyUnlimited();
    public void SetTemporarilyUnlimited(bool isTemporarilyUnlimited);
    public bool IsCurrentlyOnBattery();
    public int GetCurrentCharge();
    public int MinRange { get; }
    public int MaxRange { get; }
}