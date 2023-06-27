using System.Collections.ObjectModel;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Helpers;

namespace GHelper.DeviceControls.Fans;

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