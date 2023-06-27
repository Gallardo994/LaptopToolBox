namespace GHelper.DeviceControls.PowerLimits;

public interface IPowerLimitController
{
    // CPU Support information
    public bool SupportsCpuSustainedPowerLimit { get; }
    public bool SupportsCpuShortTermPowerLimit { get; }
    public bool SupportsCpuFastLimit { get; }
    
    // GPU Support information
    public bool SupportsGpuPowerBoost { get; }
    public bool SupportsGpuTempTarget { get; }
    
    // CPU Control
    public bool SetCpuSustainedPowerLimit(int sustainedPowerLimit);
    public bool SetCpuShortTermPowerLimit(int shortTermPowerLimit);
    public bool SetCpuFastLimit(int fastLimit);
    
    // GPU Control
    public bool SetGpuPowerBoost(int powerBoost);
    public bool SetGpuTempTarget(int tempTarget);
}