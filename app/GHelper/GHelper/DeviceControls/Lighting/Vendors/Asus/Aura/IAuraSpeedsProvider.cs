using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; }
}