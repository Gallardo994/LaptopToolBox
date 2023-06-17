using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.Aura;

public class AuraSpeedsProvider : IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; init; }

    public AuraSpeedsProvider()
    {
        var modelsTemp = new List<AuraSpeedModel>();
        
        var speeds = Enum.GetValues<AuraSpeed>();
        for (var i = 0; i < speeds.Length; i++)
        {
            var speed = speeds[i];
            var model = new AuraSpeedModel(i, speed);
            modelsTemp.Add(model);
        }
        
        SupportedSpeeds = new ObservableCollection<AuraSpeedModel>(modelsTemp);
    }
}