using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceModesProvider
{
    public ObservableCollection<IPerformanceMode> AvailableModes { get; }
    public IPerformanceMode GetNextModeAfter(IPerformanceMode currentMode);

}