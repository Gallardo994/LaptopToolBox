namespace GHelper.DeviceControls.PowerLimits;

public interface IPowerLimitRangeInfo
{
    public IPowerRange CpuSpl { get; }
    public IPowerRange CpuSppt { get; }
    public IPowerRange CpuFppt { get; }

    public IClockRange GpuCore { get; }
    public IClockRange GpuMemory { get; }
    public IPowerRange GpuPower { get; }
    public IThermalRange GpuTarget { get; }
}