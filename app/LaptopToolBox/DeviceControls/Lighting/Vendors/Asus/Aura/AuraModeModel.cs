namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public class AuraModeModel
{
    public string Title { get; set; }
    public AuraMode Mode { get; set; }
    
    public AuraModeModel(string title, AuraMode mode)
    {
        Title = title;
        Mode = mode;
    }
}