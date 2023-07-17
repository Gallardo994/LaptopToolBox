using System.ComponentModel;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.Battery;

public interface IBattery : IObservableObject
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