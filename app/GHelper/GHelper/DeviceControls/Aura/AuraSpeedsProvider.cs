using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace GHelper.DeviceControls.Aura;

public class AuraSpeedsProvider : IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; init; }

    public AuraSpeedsProvider()
    {
        var modelsTemp = new List<AuraSpeedModel>();
        
        var speeds = Enum.GetValues<AuraSpeed>();
        foreach (var speed in speeds)
        {
            var title = typeof(AuraSpeed).GetCustomAttribute(typeof(SpeedTitleAttribute)) is SpeedTitleAttribute attribute
                ? attribute.Title
                : speed.ToString();
            
            var model = new AuraSpeedModel(title, speed);
            modelsTemp.Add(model);
        }
        
        SupportedSpeeds = new ObservableCollection<AuraSpeedModel>(modelsTemp);
    }
}