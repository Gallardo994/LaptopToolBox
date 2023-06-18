using System.Collections.ObjectModel;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class PerformanceViewModel
{
    private readonly IPerformanceModesProvider _performanceModesProvider;
    
    public ObservableCollection<PerformanceMode> Modes => _performanceModesProvider.AvailableModes;
    
    public PerformanceViewModel()
    {
        _performanceModesProvider = Services.ResolutionRoot.Get<IPerformanceModesProvider>();
    }
}