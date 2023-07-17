using System.Collections.ObjectModel;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraModesProvider : IObservableObject
{
    public ObservableCollection<AuraModeModel> SupportedModes { get; }
}