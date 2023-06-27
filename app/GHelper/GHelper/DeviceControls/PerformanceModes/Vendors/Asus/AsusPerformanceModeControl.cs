using System.Linq;
using GHelper.Configs;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using GHelper.DeviceControls.Fans;
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
    private readonly IFanController _fanController;
    
    [Inject]
    public AsusPerformanceModeControl(IConfig config,
        IAcpi acpi,
        INotificationService notificationService,
        IPerformanceModesProvider performanceModesProvider,
        IFanController fanController)
    {
        _config = config;
        _acpi = acpi;
        _notificationService = notificationService;
        _performanceModesProvider = performanceModesProvider;
        _fanController = fanController;
    }

    public void SetMode(IPerformanceMode performanceMode)
    {
        var result = _acpi.DeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_THROTTLE_THERMAL_POLICY, (uint) performanceMode.Type);
        
        Log.Debug("Set performance mode result: {Result}", result);
        
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

        var result = _fanController.SetCpuFanCurve(customPerformanceMode.CpuFanCurve);
        Log.Debug("Set CPU fan curve result: {Result}", result);
        
        result = _fanController.SetGpuFanCurve(customPerformanceMode.GpuFanCurve);
        Log.Debug("Set GPU fan curve result: {Result}", result);
    }
}