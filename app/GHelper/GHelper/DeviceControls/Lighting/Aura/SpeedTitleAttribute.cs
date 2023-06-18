using System;

namespace GHelper.DeviceControls.Lighting.Aura;

public class SpeedTitleAttribute : Attribute
{
    public string Title { get; }
    
    public SpeedTitleAttribute(string title)
    {
        Title = title;
    }
}