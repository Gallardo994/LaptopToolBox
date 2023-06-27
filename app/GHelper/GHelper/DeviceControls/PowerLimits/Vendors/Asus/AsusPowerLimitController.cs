using System.Collections.Generic;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.PowerLimits.Vendors.Asus;

public class AsusPowerLimitController : IPowerLimitController
{
    private readonly IAcpi _acpi;
    
    [Inject]
    public AsusPowerLimitController(IAcpi acpi)
    {
        _acpi = acpi;
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
            var result = _acpi.DeviceGet((uint) id);
            if (result > 0)
            {
                _supportedWmiCalls.Add(id);
            }
        }
        
        Log.Debug("Supported WMI Calls: {SupportedWmiCalls}", _supportedWmiCalls);
    }
    
    // CPU Support information
    public bool SupportsCpuSustainedPowerLimit => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_SPL_PL1);
    public bool SupportsCpuShortTermPowerLimit => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_SPPT_PL2);
    public bool SupportsCpuFastLimit => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_FPPT);
    
    // GPU Support information
    public bool SupportsGpuPowerBoost => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_NVIDIA_GPU_BOOST);
    public bool SupportsGpuTempTarget => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET);
    
    // CPU Control
    public bool SetCpuSustainedPowerLimit(int sustainedPowerLimit)
    {
        if (!SupportsCpuSustainedPowerLimit)
        {
            return false;
        }
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_CPU_SPL_PL1, (uint)sustainedPowerLimit) > 0;
    }

    public bool SetCpuShortTermPowerLimit(int shortTermPowerLimit)
    {
        if (!SupportsCpuShortTermPowerLimit)
        {
            return false;
        }
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_CPU_SPPT_PL2, (uint)shortTermPowerLimit) > 0;
    }

    public bool SetCpuFastLimit(int fastLimit)
    {
        if (!SupportsCpuFastLimit)
        {
            return false;
        }
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_CPU_FPPT, (uint)fastLimit) > 0;
    }

    // GPU Control
    public bool SetGpuPowerBoost(int powerBoost)
    {
        if (!SupportsGpuPowerBoost)
        {
            return false;
        }
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_NVIDIA_GPU_BOOST, (uint)powerBoost) > 0;
    }

    public bool SetGpuTempTarget(int tempTarget)
    {
        if (!SupportsGpuTempTarget)
        {
            return false;
        }
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET, (uint)tempTarget) > 0;
    }
}