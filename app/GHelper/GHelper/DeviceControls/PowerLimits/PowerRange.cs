namespace GHelper.DeviceControls.PowerLimits;

public class PowerRange : IPowerRange
{
    public int Default { get; init; }
    public int Min { get; init; }
    public int Max { get; init; }
}