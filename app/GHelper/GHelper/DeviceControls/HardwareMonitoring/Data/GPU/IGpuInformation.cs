using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.GPU;

public interface IGpuInformation
{
    public ObservableCollection<ITemperatureSensor> Sensors { get; set; }
    
    public void Clear();
}