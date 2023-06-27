using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Fans.Vendors;

public class AsusFanController : IFanController
{
    private readonly IAcpi _acpi;

    public int FanCurvePointCount { get; } = 16;
    
    [Inject]
    public AsusFanController(IAcpi acpi)
    {
        _acpi = acpi;
    }

    public int GetCpuFanRpm()
    {
        var result = _acpi.DeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_CPU_FAN_CTRL);
        return (int) (result & (uint) AsusWmi.ASUS_WMI_DSTS_FAN_CTRL_MASK) * 100;
    }
    
    public int GetGpuFanRpm()
    {
        var result = _acpi.DeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_GPU_FAN_CTRL);
        return (int) (result - (uint) AsusWmi.ASUS_WMI_DSTS_FAN_CTRL_MASK) * 100;
    }

    public FanCurveResult SetCpuFanCurve(FanCurve fanCurve)
    {
        var validationResult = ValidateFanCurve(fanCurve);
        
        if (validationResult != FanCurveResult.OK)
        {
            return validationResult;
        }

        var byteArray = fanCurve.ToByteArray();
        
        Log.Debug("Set CPU fan curve: {ByteArray}", byteArray);
        
        //var result = _acpi.DeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_CPU_FAN_CURVE, byteArray);
        
        //Log.Debug("Set CPU fan curve result: {Result}", result);
        
        return FanCurveResult.OK;
    }

    public FanCurveResult SetGpuFanCurve(FanCurve fanCurve)
    {
        var validationResult = ValidateFanCurve(fanCurve);
        
        if (validationResult != FanCurveResult.OK)
        {
            return validationResult;
        }
        
        var byteArray = fanCurve.ToByteArray();
        
        Log.Debug("Set GPU fan curve: {ByteArray}", byteArray);
        
        //var result = _acpi.DeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_GPU_FAN_CURVE, byteArray);
        
        //Log.Debug("Set GPU fan curve result: {Result}", result);
        
        return FanCurveResult.OK;
    }
    
    public FanCurveResult ValidateFanCurve(FanCurve fanCurve)
    {
        if (fanCurve.PointCount != FanCurvePointCount)
        {
            return FanCurveResult.WrongPointCount;
        }
        
        var lastPoint = fanCurve[0];

        foreach (var fanCurvePoint in fanCurve)
        {
            if (fanCurvePoint.Value < lastPoint.Value)
            {
                return FanCurveResult.PointsNotIncreasing;
            }
            
            if (fanCurvePoint.Value >= 100)
            {
                return FanCurveResult.BeyondMaximum;
            }

            lastPoint = fanCurvePoint;
        }

        return FanCurveResult.OK;
    }
}