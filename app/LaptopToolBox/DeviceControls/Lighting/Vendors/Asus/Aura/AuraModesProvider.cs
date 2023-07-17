using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public partial class AuraModesProvider : ObservableObject, IAuraModesProvider
{
    [ObservableProperty] private ObservableCollection<AuraModeModel> _supportedModes;

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