using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public class AuraSpeedsProvider : IAuraSpeedsProvider
{
    public ObservableCollection<AuraSpeedModel> SupportedSpeeds { get; init; }

    public AuraSpeedsProvider()
    {
        var modelsTemp = new List<AuraSpeedModel>();
        
        var speeds = Enum.GetValues<AuraSpeed>();
        foreach (var speed in speeds)
        {
            var title = string.Empty;
            
            var attribute = EnumHelper.GetAttribute<AuraSpeed, SpeedTitleAttribute>(speed);
            
            if (attribute != null)
            {
                title = attribute.Title;
            }
            
            var model = new AuraSpeedModel(title, speed);
            modelsTemp.Add(model);
        }
        
        SupportedSpeeds = new ObservableCollection<AuraSpeedModel>(modelsTemp);
    }
}