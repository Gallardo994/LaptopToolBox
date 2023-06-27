using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceModesProvider
{
    public ObservableCollection<IPerformanceMode> AvailableModes { get; }
    public IPerformanceMode GetNextModeAfter(IPerformanceMode currentMode);
    public IPerformanceMode CreateCustomPerformanceMode(string title, string description = "");
    public bool DeleteCustomPerformanceMode(IPerformanceMode performanceMode);
    public IPerformanceMode ApplyModificationsFromCustomPerformanceMode(IPerformanceMode performanceMode);
}