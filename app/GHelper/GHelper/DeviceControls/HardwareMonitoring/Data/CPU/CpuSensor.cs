using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public partial class CpuSensor : ObservableObject, ICpuSensor
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private float _value;
    [ObservableProperty] private int _roundedValue;
    
    public void Clear()
    {
        Name = string.Empty;
        Value = 0;
        RoundedValue = 0;
    }
    
    public override string ToString()
    {
        return $"Name: {Name}, Value: {Value}, RoundedValue: {RoundedValue}";
    }
}