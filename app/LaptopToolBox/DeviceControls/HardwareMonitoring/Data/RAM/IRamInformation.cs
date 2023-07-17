using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Data.RAM;

public interface IRamInformation : IObservableObject
{
    public float Total { get; set; }
    public float Used { get; set; }
    public float Available { get; set; }
    public int PercentageUsed { get; set; }
    
    public void Clear();
}