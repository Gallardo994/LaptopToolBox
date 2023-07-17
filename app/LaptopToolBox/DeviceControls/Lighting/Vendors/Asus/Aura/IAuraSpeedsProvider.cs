using System.Collections.ObjectModel;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; }
}