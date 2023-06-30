using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public interface ICpuSensor : IObservableObject
{
    public string Name { get; set; }
    public float Value { get; set; }
    
    public void Clear();
}