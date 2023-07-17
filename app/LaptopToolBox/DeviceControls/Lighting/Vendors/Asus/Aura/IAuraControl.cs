using System.Drawing;

namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraControl
{
    public void Apply(AuraMode mode, Color color, Color color2, AuraSpeed speed);
    public void IncreaseBrightness();
    public void DecreaseBrightness();
}