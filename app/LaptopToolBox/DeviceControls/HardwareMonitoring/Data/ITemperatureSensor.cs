using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Data;

public interface ITemperatureSensor : IObservableObject
{
    public string Name { get; set; }
    public float Value { get; set; }
    public int RoundedValue { get; set; }
    
    public void Clear();
}