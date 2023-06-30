using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.GPU;

public interface IGpuInformation : IObservableObject
{
    public int TotalPower { get; set; }
    
    public void Clear();
}