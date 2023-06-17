using System;

namespace GHelper.DeviceControls.Aura;

public class SpeedTitleAttribute : Attribute
{
    public string Title { get; }
    
    public SpeedTitleAttribute(string title)
    {
        Title = title;
    }
}