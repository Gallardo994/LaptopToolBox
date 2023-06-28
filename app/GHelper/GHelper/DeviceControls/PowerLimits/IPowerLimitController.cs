namespace GHelper.DeviceControls.PowerLimits;

public interface IPowerLimitController
{
    public IPowerLimitRangeInfo PowerLimits { get; }
    
    // CPU Support information
    public bool SupportsCpuSpl { get; }
    public bool SupportsCpuSppt { get; }
    public bool SupportsCpuFppt { get; }

    // GPU Support information
    public bool SupportsGpuPowerBoost { get; }
    public bool SupportsGpuTempTarget { get; }

    // CPU Control
    public bool SetCpuSpl(int sustainedPowerLimit);
    public bool SetCpuSppt(int shortTermPowerLimit);
    public bool SetCpuFppt(int fastLimit);
    
    // GPU Control
    public bool SetGpuPowerBoost(int powerBoost);
    public bool SetGpuTempTarget(int tempTarget);
}