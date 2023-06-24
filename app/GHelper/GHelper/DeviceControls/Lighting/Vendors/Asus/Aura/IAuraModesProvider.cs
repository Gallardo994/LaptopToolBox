using System.Collections.ObjectModel;
using GHelper.Helpers;

namespace GHelper.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraModesProvider : IObservableObject
{
    public ObservableCollection<AuraModeModel> SupportedModes { get; }
}