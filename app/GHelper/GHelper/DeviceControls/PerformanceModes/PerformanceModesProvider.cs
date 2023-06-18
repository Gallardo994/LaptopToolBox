using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.DeviceControls.PerformanceModes;

public class PerformanceModesProvider : IPerformanceModesProvider
{
    public ObservableCollection<PerformanceMode> AvailableModes { get; init; }

    public PerformanceModesProvider()
    {
        AvailableModes = new ObservableCollection<PerformanceMode>
        {
            new PerformanceMode
            {
                Title = "Silent",
                Description = "Save battery life, reduce heat and fan noise by reducing performance",
                IsCurrent = false,
                Type = PerformanceModeType.Silent,
                Icon = new FontIcon
                {
                    Glyph = "\uEC48",
                }
            },
            new PerformanceMode
            {
                Title = "Balanced",
                Description = "Balance performance and battery life. This is the default mode",
                IsCurrent = false,
                Type = PerformanceModeType.Balanced,
                Icon = new FontIcon
                {
                    Glyph = "\uEC49",
                }
            },
            new PerformanceMode
            {
                Title = "Turbo",
                Description = "Maximize performance at the cost of battery life, temperatures and fan noise",
                IsCurrent = false,
                Type = PerformanceModeType.Turbo,
                Icon = new FontIcon
                {
                    Glyph = "\uEC4A",
                }
            }
        };
    }
    
    public PerformanceMode GetNextModeAfter(PerformanceMode currentMode)
    {
        var currentModeIndex = AvailableModes.IndexOf(currentMode);
        var nextModeIndex = (currentModeIndex + 1) % AvailableModes.Count;
        return AvailableModes[nextModeIndex];
    }
}