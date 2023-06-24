using System.ComponentModel;

namespace GHelper.DeviceControls.Battery;

public interface IBattery : INotifyPropertyChanging, INotifyPropertyChanged
{
    public bool IsBatteryLimitSupported { get; }
    public void SetBatteryLimit(int limit);
    public int GetBatteryLimit();
    public bool IsTemporarilyUnlimited { get; set; }
    public bool IsCurrentlyOnBattery();
    public int GetCurrentCharge();
    public int MinRange { get; }
    public int MaxRange { get; }
}