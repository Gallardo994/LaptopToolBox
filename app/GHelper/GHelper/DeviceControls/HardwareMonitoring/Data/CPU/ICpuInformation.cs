using System.Collections.ObjectModel;
using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public interface ICpuInformation : IObservableObject
{
    public int TotalLoad { get; set; }
    public int TotalPower { get; set; }
    public ObservableCollection<ICpuCoreInformation> CoresLoad { get; set; }
    
    public void Clear();
}