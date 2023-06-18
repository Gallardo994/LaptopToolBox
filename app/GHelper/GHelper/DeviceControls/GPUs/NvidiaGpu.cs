using System;
using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.Interfaces.GPU;

namespace GHelper.DeviceControls.GPUs;

public class NvidiaGpu : IGpuControl
{
    private readonly PhysicalGPU _physicalGpu;
    private readonly IThermalSensor _thermalSensor;
    
    public NvidiaGpu()
    {
        _physicalGpu = FindPhysicalGpu();
        _thermalSensor = GetThermalSensor();
    }

    private PhysicalGPU FindPhysicalGpu()
    {
        try
        {
            return PhysicalGPU
                .GetPhysicalGPUs()
                .FirstOrDefault(gpu => gpu.SystemType == SystemType.Laptop);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private IThermalSensor GetThermalSensor()
    {
        if (!EnsureGpuIsValid())
        {
            return null;
        }
        
        return GPUApi.GetThermalSettings(_physicalGpu.Handle).Sensors
            .FirstOrDefault(s => s.Target == ThermalSettingsTarget.GPU);
    }
    
    public bool IsAvailable()
    {
        return EnsureGpuIsValid();
    }
    
    private bool EnsureGpuIsValid()
    {
        return _physicalGpu != null && _thermalSensor != null;
    }

    public int GetTemperature()
    {
        return !EnsureGpuIsValid() ? 0 : _thermalSensor.CurrentTemperature;
    }
    
    public int GetFanRpm()
    {
        return !EnsureGpuIsValid() ? 0 : _physicalGpu.CoolerInformation.CurrentFanSpeedInRPM;
    }
}