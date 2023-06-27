using System.Linq;
using GHelper.Configs;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using GHelper.Notifications;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.PerformanceModes.Vendors.Asus;

public class AsusPerformanceModeControl : IPerformanceModeControl
{
    private readonly IConfig _config;
    private readonly IAcpi _acpi;
    private readonly INotificationService _notificationService;
    private readonly IPerformanceModesProvider _performanceModesProvider;
    
    [Inject]
    public AsusPerformanceModeControl(IConfig config,
        IAcpi acpi,
        INotificationService notificationService,
        IPerformanceModesProvider performanceModesProvider)
    {
        _config = config;
        _acpi = acpi;
        _notificationService = notificationService;
        _performanceModesProvider = performanceModesProvider;
    }

    public void SetMode(IPerformanceMode performanceMode)
    {
        var result = _acpi.DeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_THROTTLE_THERMAL_POLICY, (uint) performanceMode.Type);
        _config.PerformanceModeCurrent = performanceMode.Id;
        TrySetCustomParameters(performanceMode);
        
        _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Switched to " + performanceMode.Title);
    }

    public IPerformanceMode GetCurrentMode()
    {
        var currentPerformanceModeId = _config.PerformanceModeCurrent;
        
        var currentPerformanceMode = _performanceModesProvider.AvailableModes.FirstOrDefault(performanceMode => performanceMode.Id == currentPerformanceModeId) ??
                                     _performanceModesProvider.AvailableModes.FirstOrDefault(performanceMode => performanceMode.Type == PerformanceModeType.Balanced);

        return currentPerformanceMode;
    }
    
    public void RestoreToFallbackMode()
    {
        var mode = _performanceModesProvider.AvailableModes.FirstOrDefault(performanceMode => performanceMode.Type == PerformanceModeType.Balanced);
        SetMode(mode);
    }

    public void CycleMode()
    {
        var currentMode = GetCurrentMode();
        var nextMode = _performanceModesProvider.GetNextModeAfter(currentMode);
        
        SetMode(nextMode);
    }

    private void TrySetCustomParameters(IPerformanceMode performanceMode)
    {
        if (performanceMode is not CustomPerformanceMode customPerformanceMode)
        {
            return;
        }
        
        // TODO: Implement fan control, curves, etc.
    }
}