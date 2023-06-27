using GHelper.DeviceControls.PerformanceModes;
using GHelper.DeviceControls.PowerLimits;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class ModifyPerformanceProfileViewModel
{
    public CustomPerformanceMode Original { get; init; }
    public CustomPerformanceMode Modified { get; init; }
    public IPowerLimitController PLC { get; init; }
    
    [Inject]
    public ModifyPerformanceProfileViewModel(CustomPerformanceMode performanceMode)
    {
        Original = performanceMode;
        Modified = new CustomPerformanceMode(Original);
        PLC = Services.ResolutionRoot.Get<IPowerLimitController>();
    }
    
    public bool IsDirty()
    {
        return Modified.HasModificationsComparedTo(Original);
    }
}