using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public partial class CpuCoreInformation : ObservableObject, ICpuCoreInformation
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private int _coreNumber;
    [ObservableProperty] private int _totalLoad;

    public void Clear()
    {
        Name = string.Empty;
        CoreNumber = 0;
        TotalLoad = 0;
    }

    public override string ToString()
    {
        return $"Name: {Name}, TotalLoad: {TotalLoad}";
    }
}