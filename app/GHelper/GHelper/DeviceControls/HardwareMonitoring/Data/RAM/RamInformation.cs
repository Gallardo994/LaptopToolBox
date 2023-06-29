using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

public partial class RamInformation : ObservableObject, IRamInformation
{
    [ObservableProperty] private long _total;
    
    public void Clear()
    {
        Total = 0;
    }
}