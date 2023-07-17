using System.Collections.Generic;
using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;
using LaptopToolBox.ModelInfo;
using Ninject;
using Serilog;

namespace LaptopToolBox.DeviceControls.PowerLimits.Vendors.Asus;

public class AsusPowerLimitController : IPowerLimitController
{
    private readonly IAcpi _acpi;
    
    public IPowerLimitRangeInfo PowerLimits { get; init; }
    
    [Inject]
    public AsusPowerLimitController(IAcpi acpi, IModelInfoProvider modelInfoProvider, IAsusPowerLimitFactory asusPowerLimitFactory)
    {
        _acpi = acpi;
        PowerLimits = asusPowerLimitFactory.Resolve(modelInfoProvider);
        
        InitializeSupportedWmiCalls();
    }

    private HashSet<AsusWmi> _supportedWmiCalls;

    private void InitializeSupportedWmiCalls()
    {
        var callsToTest = new HashSet<AsusWmi>
        {
            AsusWmi.ASUS_WMI_CPU_SPL_PL1,
            AsusWmi.ASUS_WMI_CPU_SPPT_PL2,
            AsusWmi.ASUS_WMI_CPU_FPPT,

            AsusWmi.ASUS_WMI_NVIDIA_GPU_BOOST,
            AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET,
        };
        
        _supportedWmiCalls = new HashSet<AsusWmi>();
        
        foreach (var id in callsToTest)
        {
            var success = _acpi.TryDeviceGet((uint) id, out var result);
            if (success && result > 0)
            {
                _supportedWmiCalls.Add(id);
            }
        }
        
        Log.Debug("Supported WMI Calls: {SupportedWmiCalls}", _supportedWmiCalls);
    }
    
    // CPU Support information
    public bool SupportsCpuSpl => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_SPL_PL1);
    public bool SupportsCpuSppt => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_SPPT_PL2);
    public bool SupportsCpuFppt => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_FPPT);

    // GPU Support information
    public bool SupportsGpuPowerBoost => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_NVIDIA_GPU_BOOST);
    public bool SupportsGpuTempTarget => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET);

    // CPU Control
    public bool SetCpuSpl(int sustainedPowerLimit)
    {
        if (!SupportsCpuSpl)
        {
            return false;
        }
        
        if (sustainedPowerLimit < PowerLimits.CpuSpl.Min || sustainedPowerLimit > PowerLimits.CpuSpl.Max)
        {
            return false;
        }
        
        return _acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_CPU_SPL_PL1, (uint)sustainedPowerLimit, out var result) && result > 0;
    }

    public bool SetCpuSppt(int shortTermPowerLimit)
    {
        if (!SupportsCpuSppt)
        {
            return false;
        }
        
        if (shortTermPowerLimit < PowerLimits.CpuSppt.Min || shortTermPowerLimit > PowerLimits.CpuSppt.Max)
        {
            return false;
        }
        
        return _acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_CPU_SPPT_PL2, (uint)shortTermPowerLimit, out var result) && result > 0;
    }

    public bool SetCpuFppt(int fastLimit)
    {
        if (!SupportsCpuFppt)
        {
            return false;
        }
        
        if (fastLimit < PowerLimits.CpuFppt.Min || fastLimit > PowerLimits.CpuFppt.Max)
        {
            return false;
        }
        
        return _acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_CPU_FPPT, (uint)fastLimit, out var result) && result > 0;
    }

    // GPU Control
    public bool SetGpuPowerBoost(int powerBoost)
    {
        if (!SupportsGpuPowerBoost)
        {
            return false;
        }
        
        if (powerBoost < PowerLimits.GpuPower.Min || powerBoost > PowerLimits.GpuPower.Max)
        {
            return false;
        }
        
        return _acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_NVIDIA_GPU_BOOST, (uint)powerBoost, out var result) && result > 0;
    }

    public bool SetGpuTempTarget(int tempTarget)
    {
        if (!SupportsGpuTempTarget)
        {
            return false;
        }
        
        if (tempTarget < PowerLimits.GpuTarget.Min || tempTarget > PowerLimits.GpuTarget.Max)
        {
            return false;
        }
        
        return _acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET, (uint)tempTarget, out var result) && result > 0;
    }
}