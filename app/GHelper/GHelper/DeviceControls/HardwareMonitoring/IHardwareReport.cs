using System.Collections.ObjectModel;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.GPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;
using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring;

public interface IHardwareReport : IObservableObject
{
    public IRamInformation RamInformation { get; set; }
    public ICpuInformation CpuInformation { get; set; }
    public IGpuInformation GpuInformation { get; set; }
    
    public ObservableCollection<ITemperatureSensor> Sensors { get; set; }
    
    public void Clear();
}