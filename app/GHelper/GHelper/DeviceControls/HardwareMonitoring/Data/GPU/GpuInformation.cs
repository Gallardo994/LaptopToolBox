using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.GPU;

public partial class GpuInformation : ObservableObject, IGpuInformation
{
    [ObservableProperty] private int _totalPower;
    
    public GpuInformation()
    {
        TotalPower = 0;
    }
    
    public void Clear()
    {
        TotalPower = 0;
    }
}