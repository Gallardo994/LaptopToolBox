namespace GHelper.DeviceControls.PowerLimits;

public interface IPowerLimitController
{
    // CPU Support information
    public bool SupportsCpuSpl { get; }
    public bool SupportsCpuSppt { get; }
    public bool SupportsCpuFppt { get; }
    public int MinCpuPowerLimit { get; }
    public int MaxCpuPowerLimit { get; }
    public int DefaultCpuPowerLimit { get; }
    
    // GPU Support information
    public bool SupportsGpuPowerBoost { get; }
    public bool SupportsGpuTempTarget { get; }
    public int MinGpuPowerBoost { get; }
    public int MaxGpuPowerBoost { get; }
    public int DefaultGpuPowerBoost { get; }
    public int MinGpuTempTarget { get; }
    public int MaxGpuTempTarget { get; }
    public int DefaultGpuTempTarget { get; }

    // CPU Control
    public bool SetCpuSpl(int sustainedPowerLimit);
    public bool SetCpuSppt(int shortTermPowerLimit);
    public bool SetCpuFppt(int fastLimit);
    
    // GPU Control
    public bool SetGpuPowerBoost(int powerBoost);
    public bool SetGpuTempTarget(int tempTarget);
}