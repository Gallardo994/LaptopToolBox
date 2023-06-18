using Microsoft.UI.Xaml.Controls;

namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceMode
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IconElement Icon { get; set; }
    
    public PerformanceModeType Type { get; set; }
    
    public bool IsCurrent { get; set; }
}