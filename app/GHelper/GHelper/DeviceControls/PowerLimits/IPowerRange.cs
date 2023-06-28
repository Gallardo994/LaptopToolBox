namespace GHelper.DeviceControls.PowerLimits;

public interface IPowerRange
{
    public int Default { get; }
    public int Min { get; }
    public int Max { get; }
}