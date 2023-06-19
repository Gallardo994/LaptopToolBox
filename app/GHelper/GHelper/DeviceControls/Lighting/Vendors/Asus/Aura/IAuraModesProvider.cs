using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraModesProvider
{
    public ObservableCollection<AuraModeModel> SupportedModes { get; }
}