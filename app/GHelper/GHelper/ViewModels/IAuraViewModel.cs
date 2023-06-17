using System.Drawing;
using GHelper.DeviceControls.Aura;

namespace GHelper.ViewModels;

public interface IAuraViewModel
{
    public AuraMode Mode { get; set; }
    public Color Color { get; set; }
    public Color Color2 { get; set; }
    public AuraSpeed Speed { get; set; }
}