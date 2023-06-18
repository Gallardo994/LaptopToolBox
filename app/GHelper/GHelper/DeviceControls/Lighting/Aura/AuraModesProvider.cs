using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Lighting.Aura;

public class AuraModesProvider : IAuraModesProvider
{
    public ObservableCollection<AuraModeModel> SupportedModes { get; init; }

    public AuraModesProvider()
    {
        var modeModelsTemp = new List<AuraModeModel>();
        
        foreach (var mode in Enum.GetValues<AuraMode>())
        {
            var modeModel = new AuraModeModel(mode.ToString(), mode);
            modeModelsTemp.Add(modeModel);
        }
        
        SupportedModes = new ObservableCollection<AuraModeModel>(modeModelsTemp);
    }
}