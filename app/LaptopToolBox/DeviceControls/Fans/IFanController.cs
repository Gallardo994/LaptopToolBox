using System.Collections.ObjectModel;
using LaptopToolBox.DeviceControls.PerformanceModes;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.Fans;

public interface IFanController : IObservableObject
{
    public int MinControlTemp { get; }
    public int MaxControlTemp { get; }
    public int FanCurvePointCount { get; }
    
    public ObservableCollection<FanCurve> IntegratedCpuFanCurves { get; }
    public ObservableCollection<FanCurve> IntegratedGpuFanCurves { get; }
    
    public int GetCpuFanRpm();
    public int GetGpuFanRpm();
    
    public FanCurveResult SetCpuFanCurve(FanCurve fanCurve);
    public FanCurveResult SetGpuFanCurve(FanCurve fanCurve);
    
    public FanCurveResult ValidateFanCurve(FanCurve fanCurve);
}