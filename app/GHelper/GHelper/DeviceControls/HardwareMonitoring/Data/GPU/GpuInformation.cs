using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.GPU;

public partial class GpuInformation : ObservableObject, IGpuInformation
{
    [ObservableProperty] private ObservableCollection<ITemperatureSensor> _sensors;
    
    public GpuInformation()
    {
        Sensors = new ObservableCollection<ITemperatureSensor>();
    }
    
    public void Clear()
    {
        foreach (var sensor in Sensors)
        {
            sensor.Clear();
        }
    }
}