using System;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public class SpeedTitleAttribute : Attribute
{
    public string Title { get; }
    
    public SpeedTitleAttribute(string title)
    {
        Title = title;
    }
}