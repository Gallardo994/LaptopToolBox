namespace GHelper.DeviceControls.PowerLimits;

public interface IPowerLimitRangeInfo
{
    public IPowerRange CpuSpl { get; }
    public IPowerRange CpuSppt { get; }
    public IPowerRange CpuFppt { get; }
    
    public IPowerRange GpuPower { get; }
    public IThermalRange GpuTarget { get; }
}