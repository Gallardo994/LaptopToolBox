namespace GHelper.DeviceControls.Fans;

public interface IFanController
{
    public int FanCurvePointCount { get; }
    
    public int GetCpuFanRpm();
    public int GetGpuFanRpm();
    
    public FanCurveResult SetCpuFanCurve(FanCurve fanCurve);
    public FanCurveResult SetGpuFanCurve(FanCurve fanCurve);
    
    public FanCurveResult ValidateFanCurve(FanCurve fanCurve);
}