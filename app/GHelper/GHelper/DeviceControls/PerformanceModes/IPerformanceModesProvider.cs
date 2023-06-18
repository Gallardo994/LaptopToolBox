using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceModesProvider
{
    public ObservableCollection<PerformanceMode> AvailableModes { get; }
    public PerformanceMode GetNextModeAfter(PerformanceMode currentMode);

}