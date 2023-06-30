using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data;

public partial class PowerConsumer : ObservableObject, IPowerConsumer
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private float _value;
    [ObservableProperty] private float _maxValue;
    [ObservableProperty] private int _roundedValue;
    [ObservableProperty] private int _roundedMaxValue;

    public PowerConsumer()
    {
        Clear();
    }

    public void Clear()
    {
        Name = string.Empty;
        Value = 0;
        MaxValue = 1;
        RoundedValue = 0;
        RoundedMaxValue = 1;
    }
}