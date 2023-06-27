using GHelper.DeviceControls.PerformanceModes;
using Ninject;

namespace GHelper.ViewModels;

public class ModifyPerformanceProfileViewModel
{
    public CustomPerformanceMode Original { get; init; }
    public CustomPerformanceMode Modified { get; init; }
    
    [Inject]
    public ModifyPerformanceProfileViewModel(CustomPerformanceMode performanceMode)
    {
        Original = performanceMode;
        Modified = new CustomPerformanceMode(Original);
    }
}