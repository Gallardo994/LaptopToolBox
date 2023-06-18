using System.Drawing;

namespace GHelper.DeviceControls.Lighting.Aura;

public interface IAuraControl
{
    public void Apply(AuraMode mode, Color color, Color color2, AuraSpeed speed);
}