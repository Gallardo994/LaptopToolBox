using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Aura;

public interface IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; }
}