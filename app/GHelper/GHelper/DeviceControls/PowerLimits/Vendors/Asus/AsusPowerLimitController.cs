﻿using System.Collections.Generic;
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
    public bool SupportsCpuSpl => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_SPL_PL1);
    public bool SupportsCpuSppt => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_SPPT_PL2);
    public bool SupportsCpuFppt => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_CPU_FPPT);
    public int MinCpuPowerLimit => 5;
    public int MaxCpuPowerLimit => 130;
    public int DefaultCpuPowerLimit => 80;
    
    // GPU Support information
    public bool SupportsGpuPowerBoost => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_NVIDIA_GPU_BOOST);
    public bool SupportsGpuTempTarget => _supportedWmiCalls.Contains(AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET);
    public int MinGpuPowerBoost => 0;
    public int MaxGpuPowerBoost => 25;
    public int DefaultGpuPowerBoost => 0;
    public int MinGpuTempTarget => 75;
    public int MaxGpuTempTarget => 87;
    public int DefaultGpuTempTarget => 87;
    
    // CPU Control
    public bool SetCpuSpl(int sustainedPowerLimit)
    {
        if (!SupportsCpuSpl)
        {
            return false;
        }
        
        if (sustainedPowerLimit < MinCpuPowerLimit || sustainedPowerLimit > MaxCpuPowerLimit)
        {
            return false;
        }
        
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_CPU_SPL_PL1, (uint)sustainedPowerLimit) > 0;
    }

    public bool SetCpuSppt(int shortTermPowerLimit)
    {
        if (!SupportsCpuSppt)
        {
            return false;
        }
        
        if (shortTermPowerLimit < MinCpuPowerLimit || shortTermPowerLimit > MaxCpuPowerLimit)
        {
            return false;
        }
        
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_CPU_SPPT_PL2, (uint)shortTermPowerLimit) > 0;
    }

    public bool SetCpuFppt(int fastLimit)
    {
        if (!SupportsCpuFppt)
        {
            return false;
        }
        
        if (fastLimit < MinCpuPowerLimit || fastLimit > MaxCpuPowerLimit)
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
        
        if (powerBoost < MinGpuPowerBoost || powerBoost > MaxGpuPowerBoost)
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
        
        if (tempTarget < MinGpuTempTarget || tempTarget > MaxGpuTempTarget)
        {
            return false;
        }
        
        return _acpi.DeviceSet((uint)AsusWmi.ASUS_WMI_NVIDIA_GPU_TEMP_TARGET, (uint)tempTarget) > 0;
    }
}