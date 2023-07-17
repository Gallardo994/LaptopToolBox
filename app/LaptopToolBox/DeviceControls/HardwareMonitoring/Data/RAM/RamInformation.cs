using CommunityToolkit.Mvvm.ComponentModel;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Data.RAM;

public partial class RamInformation : ObservableObject, IRamInformation
{
    [ObservableProperty] private float _total;
    [ObservableProperty] private float _used;
    [ObservableProperty] private float _available;
    [ObservableProperty] private int _percentageUsed;
    
    public void Clear()
    {
        Total = 0;
        Used = 0;
        Available = 0;
        PercentageUsed = 0;
    }

    public override string ToString()
    {
        return $"Total: {Total} Used: {Used} Available: {Available} Percentage Used: {PercentageUsed}";
    }
}