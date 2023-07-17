using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.PerformanceModes;
using Ninject;
using Serilog;
using System.Linq;
using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;
using LaptopToolBox.Serialization;

namespace LaptopToolBox.DeviceControls.Fans.Vendors;

public partial class AsusFanController : ObservableObject, IFanController
{
    private readonly IAcpi _acpi;

    public int MinControlTemp { get; } = 40;
    public int MaxControlTemp { get; } = 100;
    public int FanCurvePointCount { get; } = 8;

    private const int IntegratedCurvesCount = 3;

    [ObservableProperty] private ObservableCollection<FanCurve> _integratedCpuFanCurves;
    [ObservableProperty] private ObservableCollection<FanCurve> _integratedGpuFanCurves;

    [Inject]
    public AsusFanController(IAcpi acpi)
    {
        _acpi = acpi;
        
        _integratedCpuFanCurves = ReadIntegratedFanCurves((uint) AsusWmi.ASUS_WMI_DEVID_CPU_FAN_CURVE);
        Log.Debug("Integrated CPU Fan Curves: {Curves}", _integratedCpuFanCurves.Count);
        
        _integratedGpuFanCurves = ReadIntegratedFanCurves((uint) AsusWmi.ASUS_WMI_DEVID_GPU_FAN_CURVE);
        Log.Debug("Integrated GPU Fan Curves: {Curves}", _integratedGpuFanCurves.Count);
    }

    private ObservableCollection<FanCurve> ReadIntegratedFanCurves(uint deviceId)
    {
        var curves = new ObservableCollection<FanCurve>();
        
        for (var index = 0U; index < IntegratedCurvesCount; index++)
        {
            var result = _acpi.DeviceGetBuffer(deviceId, index);
            var deserializer = new BinaryDeserializer(result);

            var response = deserializer.ReadUint();
            
            Log.Debug("ReadIntegratedFanCurves: {result}", result);
            if (response == 0)
            {
                break;
            }
            
            var fanCurve = new FanCurve(FanCurvePointCount);
            
            for (var i = 0; i < FanCurvePointCount; i++)
            {
                var temperature = deserializer.ReadByte();
                fanCurve[i].Temperature = temperature;
            }
            
            for (var i = 0; i < FanCurvePointCount; i++)
            {
                var value = deserializer.ReadByte();
                fanCurve[i].Value = value;
            }
            
            curves.Add(fanCurve);
        }
        
        return curves;
    }

    private void PrintFanCurve(string prefix, byte[] result, int readCount)
    {
        var template = "{prefix} byte[{readCount}] = {{ {Bytes} }}";
        var bytes = string.Join(", ", result.Take(readCount).Select(b => $"0x{b:X2}"));
        var decimals = string.Join(", ", result.Take(readCount).Select(b => $"{b}"));
        Log.Debug(template, prefix, readCount, bytes);
        Log.Debug(template, prefix, readCount, decimals);
    }

    public int GetCpuFanRpm()
    {
        var success = _acpi.TryDeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_CPU_FAN_CTRL, out var result);
        return success ? (int) (result & (uint) AsusWmi.ASUS_WMI_DSTS_FAN_CTRL_MASK) * 100 : 0;
    }

    public int GetGpuFanRpm()
    {
        var success = _acpi.TryDeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_GPU_FAN_CTRL, out var result);
        return success ? (int) (result - (uint) AsusWmi.ASUS_WMI_DSTS_FAN_CTRL_MASK) * 100 : 0;
    }

    public FanCurveResult SetCpuFanCurve(FanCurve fanCurve)
    {
        var validationResult = ValidateFanCurve(fanCurve);
        
        if (validationResult != FanCurveResult.OK)
        {
            return validationResult;
        }

        var byteArray = fanCurve.ToByteArray();
        var success = _acpi.TryDeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_CPU_FAN_CURVE, byteArray, out var result);

        PrintFanCurve("AFTER_SET_CPU", byteArray, FanCurvePointCount * 2);
        
        return success && result == 1 ? FanCurveResult.OK : FanCurveResult.BiosRejected;
    }

    public FanCurveResult SetGpuFanCurve(FanCurve fanCurve)
    {
        var validationResult = ValidateFanCurve(fanCurve);
        
        if (validationResult != FanCurveResult.OK)
        {
            return validationResult;
        }
        
        var byteArray = fanCurve.ToByteArray();
        var success = _acpi.TryDeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_GPU_FAN_CURVE, byteArray, out var result);
        
        PrintFanCurve("AFTER_SET_GPU", byteArray, FanCurvePointCount * 2);
        
        return success && result == 1 ? FanCurveResult.OK : FanCurveResult.BiosRejected;
    }
    
    public FanCurveResult ValidateFanCurve(FanCurve fanCurve)
    {
        if (fanCurve.PointCount != FanCurvePointCount)
        {
            return FanCurveResult.WrongPointCount;
        }
        
        var lastPoint = fanCurve[0];
        
        var isAllZero = fanCurve.All(fanCurvePoint => fanCurvePoint.Value == 0);
        if (isAllZero)
        {
            return FanCurveResult.AllPointsZero;
        }
        
        var containsBeyondMaximum = fanCurve.Any(fanCurvePoint => fanCurvePoint.Value >= 100);
        if (containsBeyondMaximum)
        {
            return FanCurveResult.BeyondMaximum;
        }

        foreach (var fanCurvePoint in fanCurve)
        {
            if (fanCurvePoint.Value < lastPoint.Value)
            {
                return FanCurveResult.PointsNotIncreasing;
            }

            lastPoint = fanCurvePoint;
        }

        return FanCurveResult.OK;
    }
}