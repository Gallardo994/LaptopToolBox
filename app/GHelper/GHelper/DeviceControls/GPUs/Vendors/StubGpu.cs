namespace GHelper.DeviceControls.GPUs.Vendors;

public class StubGpu : IGpuControl
{
    public bool SupportsCoreOverclock => false;
    public bool SupportsMemoryOverclock => false;
    
    public int GetTemperature()
    {
        return 0;
    }

    public void SetCoreClockOffset(int offset)
    {
        return;
    }

    public int GetCoreClockOffset()
    {
        return 0;
    }

    public void SetMemoryClockOffset(int offset)
    {
        return;
    }

    public int GetMemoryClockOffset()
    {
        return 0;
    }
}