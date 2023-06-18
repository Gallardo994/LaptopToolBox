using Microsoft.UI.Xaml.Controls;

namespace GHelper.DeviceControls.PerformanceModes;

public class IntegratedPerformanceMode : IPerformanceMode
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IconElement Icon { get; set; }
    public PerformanceModeType Type { get; init; }
}