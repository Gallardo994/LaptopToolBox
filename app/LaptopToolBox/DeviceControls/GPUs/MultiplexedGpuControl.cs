using System.Linq;
using LaptopToolBox.DeviceControls.GPUs.Vendors.Nvidia;

namespace LaptopToolBox.DeviceControls.GPUs;

public class MultiplexedGpuControl : IGpuControl
{
    private readonly IGpuControl[] _gpuControls;
    
    public MultiplexedGpuControl()
    {
        _gpuControls = new IGpuControl[]
        {
            new NvidiaGpu(),
            new StubGpu()
        };
    }
    
    public bool IsAvailable()
    {
        return TryGetActiveGpuControl(out _);
    }
    
    private IGpuControl GetActiveGpuControl()
    {
        return _gpuControls.FirstOrDefault(gpuControl => gpuControl.IsAvailable());
    }
    
    private bool TryGetActiveGpuControl(out IGpuControl gpuControl)
    {
        gpuControl = GetActiveGpuControl();
        return gpuControl != null;
    }
    
    public int GetTemperature()
    {
        return !TryGetActiveGpuControl(out var gpuControl) ? 0 : gpuControl.GetTemperature();
    }

    public bool SupportsCoreOverclock => TryGetActiveGpuControl(out var gpuControl) && gpuControl.SupportsCoreOverclock;
    public void SetCoreClockOffset(int offset)
    {
        if (!TryGetActiveGpuControl(out var gpuControl))
        {
            return;
        }
        
        gpuControl.SetCoreClockOffset(offset);
    }

    public int GetCoreClockOffset()
    {
        return !TryGetActiveGpuControl(out var gpuControl) ? 0 : gpuControl.GetCoreClockOffset();
    }

    public bool SupportsMemoryOverclock => TryGetActiveGpuControl(out var gpuControl) && gpuControl.SupportsMemoryOverclock;
    public void SetMemoryClockOffset(int offset)
    {
        if (!TryGetActiveGpuControl(out var gpuControl))
        {
            return;
        }
        
        gpuControl.SetMemoryClockOffset(offset);
    }

    public int GetMemoryClockOffset()
    {
        return !TryGetActiveGpuControl(out var gpuControl) ? 0 : gpuControl.GetMemoryClockOffset();
    }
}