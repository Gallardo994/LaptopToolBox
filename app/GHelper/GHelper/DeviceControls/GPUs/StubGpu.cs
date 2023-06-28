namespace GHelper.DeviceControls.GPUs;

public class StubGpu : IGpuControl
{
    public bool SupportsCoreOverclock => false;
    public bool SupportsMemoryOverclock => false;

    public bool IsAvailable()
    {
        return true;
    }

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