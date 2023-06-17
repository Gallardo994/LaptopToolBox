using System.Drawing;

namespace GHelper.DeviceControls.Aura;

public interface IAuraController
{
    public AuraMode Mode { get; set; }
    public Color Color { get; set; }
    public Color Color2 { get; set; }
    public AuraSpeed Speed { get; set; }
    
    public bool IsScopeActive { get; internal set; }
    public AuraControllerScope Scope();
    public bool Refresh();
}