using System.Collections.ObjectModel;

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
                Description = "Silent description",
                IsCurrent = false,
                Type = PerformanceModeType.Silent,
            },
            new PerformanceMode
            {
                Title = "Balanced",
                Description = "Balanced description",
                IsCurrent = false,
                Type = PerformanceModeType.Balanced,
            },
            new PerformanceMode
            {
                Title = "Turbo",
                Description = "Turbo description",
                IsCurrent = false,
                Type = PerformanceModeType.Turbo,
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