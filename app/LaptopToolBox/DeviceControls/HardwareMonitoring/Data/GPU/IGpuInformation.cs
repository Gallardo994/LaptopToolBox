using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Data.GPU;

public interface IGpuInformation : IObservableObject
{
    public int TotalPower { get; set; }
    
    public void Clear();
}