using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Lighting.Aura;

public interface IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; }
}