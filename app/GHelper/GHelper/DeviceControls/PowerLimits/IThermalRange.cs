namespace GHelper.DeviceControls.PowerLimits;

public interface IThermalRange
{
    public int Default { get; init; }
    public int Min { get; init; }
    public int Max { get; init; }
}