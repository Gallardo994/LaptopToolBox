using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Aura;

public interface IAuraModesProvider
{
    public ObservableCollection<AuraModeModel> SupportedModes { get; }
}