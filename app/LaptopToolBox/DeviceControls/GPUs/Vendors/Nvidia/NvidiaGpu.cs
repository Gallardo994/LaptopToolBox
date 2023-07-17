using System;
using System.Linq;
using NvAPIWrapper.GPU;
using NvAPIWrapper.Native;
using NvAPIWrapper.Native.Exceptions;
using NvAPIWrapper.Native.GPU;
using NvAPIWrapper.Native.GPU.Structures;
using NvAPIWrapper.Native.Interfaces.GPU;
using Serilog;

namespace LaptopToolBox.DeviceControls.GPUs.Vendors.Nvidia;

public class NvidiaGpu : IGpuControl
{
    private PhysicalGPU _physicalGpu;

    public bool SupportsCoreOverclock => EnsureGpuIsValid();
    public bool SupportsMemoryOverclock => EnsureGpuIsValid();
    
    public NvidiaGpu()
    {
        
    }

    private PhysicalGPU FindPhysicalGpu()
    {
        try
        {
            var gpu = PhysicalGPU
                .GetPhysicalGPUs()
                .FirstOrDefault(gpu => gpu.SystemType == SystemType.Laptop);
            
            Log.Information("Found physical GPU: {Gpu}", gpu);
            
            return gpu;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to find physical GPU");
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
        if (_physicalGpu != null && IsDeviceAccessible(_physicalGpu))
        {
            return true;
        }
        
        _physicalGpu = FindPhysicalGpu();
        return _physicalGpu != null && IsDeviceAccessible(_physicalGpu);
    }

    private bool IsDeviceAccessible(PhysicalGPU gpu)
    {
        try
        {
            var _ = gpu.GPUId;
            return true;
        }
        catch (NVIDIAApiException ex)
        {
            Log.Error(ex, "Failed to access GPU");
            return false;
        }
    }

    public int GetTemperature()
    {
        if (!EnsureGpuIsValid())
        {
            return 0;
        }
        
        return GetThermalSensor() is not { } sensor ? 0 : sensor.CurrentTemperature;
    }
    
    public int GetFanRpm()
    {
        return !EnsureGpuIsValid() ? 0 : _physicalGpu.CoolerInformation.CurrentFanSpeedInRPM;
    }
    
    public int GetCoreClockOffset()
    {
        if (!EnsureGpuIsValid())
        {
            return 0;
        }
        
        var states = GPUApi.GetPerformanceStates20(_physicalGpu.Handle);
        
        if (!states.Clocks.TryGetValue(PerformanceStateId.P0_3DPerformance, out var p0))
        {
            return 0;
        }
        
        var p0CoreClock = p0[0].FrequencyDeltaInkHz.DeltaValue / 1000;
        
        return p0CoreClock;
    }
    
    public int GetMemoryClockOffset()
    {
        if (!EnsureGpuIsValid())
        {
            return 0;
        }
        
        var states = GPUApi.GetPerformanceStates20(_physicalGpu.Handle);
        
        if (!states.Clocks.TryGetValue(PerformanceStateId.P0_3DPerformance, out var p0))
        {
            return 0;
        }
        
        var p0MemoryClock = p0[1].FrequencyDeltaInkHz.DeltaValue / 1000;
        
        return p0MemoryClock;
    }
    
    public void SetCoreClockOffset(int coreOffset)
    {
        if (!EnsureGpuIsValid())
        {
            return;
        }
        
        var states = GPUApi.GetPerformanceStates20(_physicalGpu.Handle);
        
        if (!states.Clocks.TryGetValue(PerformanceStateId.P0_3DPerformance, out var p0))
        {
            return;
        }

        var clocks = new PerformanceStates20ClockEntryV1[]
        {
            new(PublicClockDomain.Graphics, new PerformanceStates20ParameterDelta(coreOffset * 1000)),
            new(PublicClockDomain.Memory, new PerformanceStates20ParameterDelta(p0[1].FrequencyDeltaInkHz.DeltaValue)),
        };

        ApplyClocks(clocks);
    }
    
    public void SetMemoryClockOffset(int memoryOffset)
    {
        if (!EnsureGpuIsValid())
        {
            return;
        }
        
        var states = GPUApi.GetPerformanceStates20(_physicalGpu.Handle);
        
        if (!states.Clocks.TryGetValue(PerformanceStateId.P0_3DPerformance, out var p0))
        {
            return;
        }

        var clocks = new PerformanceStates20ClockEntryV1[]
        {
            new(PublicClockDomain.Graphics, new PerformanceStates20ParameterDelta(p0[0].FrequencyDeltaInkHz.DeltaValue)),
            new(PublicClockDomain.Memory, new PerformanceStates20ParameterDelta(memoryOffset * 1000)),
        };

        ApplyClocks(clocks);
    }

    private void ApplyClocks(PerformanceStates20ClockEntryV1[] clocks)
    {
        var voltages = Array.Empty<PerformanceStates20BaseVoltageEntryV1>();
        
        var states = new PerformanceStates20InfoV1.PerformanceState20[]
        {
            new(PerformanceStateId.P0_3DPerformance, clocks, voltages)
        };
        
        var overclock = new PerformanceStates20InfoV1(states, 2, 0);
        
        GPUApi.SetPerformanceStates20(_physicalGpu.Handle, overclock);
    }
}