using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;

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
        var result = ValidateFanCurve(fanCurve);
        
        if (result != FanCurveResult.OK)
        {
            return result;
        }
        
        throw new System.NotImplementedException();
    }

    public FanCurveResult SetGpuFanCurve(FanCurve fanCurve)
    {
        var result = ValidateFanCurve(fanCurve);
        
        if (result != FanCurveResult.OK)
        {
            return result;
        }
        
        throw new System.NotImplementedException();
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