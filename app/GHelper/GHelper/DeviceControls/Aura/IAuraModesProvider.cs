namespace GHelper.DeviceControls.Aura;

public interface IAuraModesProvider
{
    public AuraMode[] SupportedModes { get; }
}