namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public class AuraSpeedModel
{
    public string Title { get; set; }
    public AuraSpeed Speed { get; set; }
    
    public AuraSpeedModel(string title, AuraSpeed speed)
    {
        Speed = speed;
        Title = title;
    }
}