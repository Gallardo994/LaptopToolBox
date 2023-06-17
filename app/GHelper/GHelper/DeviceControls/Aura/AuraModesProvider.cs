using System;

namespace GHelper.DeviceControls.Aura;

public class AuraModesProvider : IAuraModesProvider
{
    public AuraMode[] SupportedModes { get; init; }

    public AuraModesProvider()
    {
        SupportedModes = Enum.GetValues<AuraMode>();
    }
}