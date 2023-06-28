using GHelper.DeviceControls.Fans;
using GHelper.DeviceControls.GPUs;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.DeviceControls.PowerLimits;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class ModifyPerformanceProfileViewModel
{
    public CustomPerformanceMode Original { get; init; }
    public CustomPerformanceMode Modified { get; private set; }
    public IPowerLimitController PowerLimitController { get; init; } = Services.ResolutionRoot.Get<IPowerLimitController>();
    public IFanController FanController { get; init; } = Services.ResolutionRoot.Get<IFanController>();
    public IGpuControl GpuControl { get; init; } = Services.ResolutionRoot.Get<IGpuControl>();
    private readonly IPerformanceModesProvider _performanceModesProvider = Services.ResolutionRoot.Get<IPerformanceModesProvider>();
    private readonly IPerformanceModeControl _performanceModeControl = Services.ResolutionRoot.Get<IPerformanceModeControl>();
    
    [Inject]
    public ModifyPerformanceProfileViewModel(CustomPerformanceMode performanceMode)
    {
        Original = performanceMode;
        Modified = new CustomPerformanceMode(Original);
    }
    
    public bool IsDirty()
    {
        return Modified.HasModificationsComparedTo(Original);
    }
    
    public void ApplyModificationsFromCustomPerformanceMode()
    {
        var appliedMode = _performanceModesProvider.ApplyModificationsFromCustomPerformanceMode(Modified);

        var currentMode = _performanceModeControl.GetCurrentMode();

        if (currentMode == appliedMode)
        {
            _performanceModeControl.SetMode(appliedMode);
        }
    }
    
    public void UndoModifications()
    {
        Original.CopyTo(Modified);
    }
}