using System.Collections.ObjectModel;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.CPU;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.GPU;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.RAM;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring;

public interface IHardwareReport : IObservableObject
{
    public IRamInformation RamInformation { get; set; }
    public ICpuInformation CpuInformation { get; set; }
    public IGpuInformation GpuInformation { get; set; }
    
    public ObservableCollection<ITemperatureSensor> Sensors { get; set; }
    public ObservableCollection<IPowerConsumer> PowerConsumers { get; set; }
    
    public void Clear();
}