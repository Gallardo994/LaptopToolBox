namespace GHelper.DeviceControls.GPUs;

public interface IGpuControl
{
    public int GetTemperature();
    
    public void SetCoreClockOffset(int offset);
    public int GetCoreClockOffset();
    
    public void SetMemoryClockOffset(int offset);
    public int GetMemoryClockOffset();
}