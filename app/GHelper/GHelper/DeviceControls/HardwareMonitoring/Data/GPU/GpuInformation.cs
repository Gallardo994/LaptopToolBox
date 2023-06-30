using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.GPU;

public partial class GpuInformation : ObservableObject, IGpuInformation
{
    [ObservableProperty] private int _totalPower;
    [ObservableProperty] private ObservableCollection<ITemperatureSensor> _sensors;
    
    public GpuInformation()
    {
        TotalPower = 0;
        Sensors = new ObservableCollection<ITemperatureSensor>();
    }
    
    public void Clear()
    {
        TotalPower = 0;
        
        foreach (var sensor in Sensors)
        {
            sensor.Clear();
        }
    }
}