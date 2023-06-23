using System.Linq;
using GHelper.Configs;
using GHelper.DeviceControls.Acpi;
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

    private const uint DeviceId = 0x00120075;
    
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
        var result = _acpi.DeviceSet(DeviceId, (int) performanceMode.Type);
        
        Log.Debug("SetMode: {DeviceId} {PerformanceModeType} {Result}", DeviceId, performanceMode.Type, result);
        
        if (result > 0)
        {
            _config.PerformanceModeCurrent = performanceMode.Id;
            _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Switched to " + performanceMode.Title);
            
            TrySetCustomParameters(performanceMode);
        }
        else
        {
            _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Failed to switch to " + performanceMode.Title);
        }
    }

    public IPerformanceMode GetCurrentMode()
    {
        var currentPerformanceModeId = _config.PerformanceModeCurrent;
        
        var currentPerformanceMode = _performanceModesProvider.AvailableModes.FirstOrDefault(performanceMode => performanceMode.Id == currentPerformanceModeId) ??
                                     _performanceModesProvider.AvailableModes.FirstOrDefault(performanceMode => performanceMode.Type == PerformanceModeType.Balanced);

        return currentPerformanceMode;
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