using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

public interface IRamInformation : IObservableObject
{
    public long Total { get; set; }
    
    public void Clear();
}