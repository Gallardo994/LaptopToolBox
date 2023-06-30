using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

public partial class RamInformation : ObservableObject, IRamInformation
{
    [ObservableProperty] private float _total;
    [ObservableProperty] private float _used;
    [ObservableProperty] private float _available;
    
    public void Clear()
    {
        Total = 0;
        Used = 0;
        Available = 0;
    }

    public override string ToString()
    {
        return $"Total: {Total} Used: {Used} Available: {Available}";
    }
}