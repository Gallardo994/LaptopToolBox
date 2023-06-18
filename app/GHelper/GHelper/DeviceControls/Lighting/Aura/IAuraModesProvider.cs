using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Lighting.Aura;

public interface IAuraModesProvider
{
    public ObservableCollection<AuraModeModel> SupportedModes { get; }
}