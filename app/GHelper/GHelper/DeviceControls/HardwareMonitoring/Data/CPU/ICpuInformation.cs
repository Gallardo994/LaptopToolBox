using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public interface ICpuInformation
{
    public int TotalLoad { get; set; }
    public ObservableCollection<ICpuCoreInformation> CoresLoad { get; set; }
    public ObservableCollection<ICpuSensor> Sensors { get; set; }
    
    public void Clear();
}