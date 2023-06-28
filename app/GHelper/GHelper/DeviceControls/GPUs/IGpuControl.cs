namespace GHelper.DeviceControls.GPUs;

public interface IGpuControl
{
    public bool IsAvailable();
    public int GetTemperature();

    public bool SupportsCoreOverclock { get; }
    public void SetCoreClockOffset(int offset);
    public int GetCoreClockOffset();
    
    public bool SupportsMemoryOverclock { get; }
    public void SetMemoryClockOffset(int offset);
    public int GetMemoryClockOffset();
}