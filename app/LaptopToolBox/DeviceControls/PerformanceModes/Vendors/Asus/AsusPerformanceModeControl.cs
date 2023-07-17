using System.Linq;
using LaptopToolBox.Injection;
using LaptopToolBox.Configs;
using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;
using LaptopToolBox.DeviceControls.Fans;
using LaptopToolBox.DeviceControls.GPUs;
using LaptopToolBox.DeviceControls.PowerLimits;
using LaptopToolBox.Notifications;
using Ninject;
using Serilog;

namespace LaptopToolBox.DeviceControls.PerformanceModes.Vendors.Asus;

public class AsusPerformanceModeControl : IPerformanceModeControl
{
    private readonly IConfig _config;
    private readonly IAcpi _acpi;
    private readonly INotificationService _notificationService;
    private readonly IPerformanceModesProvider _performanceModesProvider;
    private readonly IFanController _fanController;
    private readonly IPowerLimitController _powerLimitController;
    private readonly IGpuControl _gpuControl;
    
    [Inject]
    public AsusPerformanceModeControl(IConfig config,
        IAcpi acpi,
        INotificationService notificationService,
        IPerformanceModesProvider performanceModesProvider,
        IFanController fanController,
        IPowerLimitController powerLimitController,
        IGpuControl gpuControl)
    {
        _config = config;
        _acpi = acpi;
        _notificationService = notificationService;
        _performanceModesProvider = performanceModesProvider;
        _fanController = fanController;
        _powerLimitController = powerLimitController;
        _gpuControl = gpuControl;
    }

    public void SetMode(IPerformanceMode performanceMode)
    {
        TryResetCustomParameters(GetCurrentMode());
        
        var success = _acpi.TryDeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_THROTTLE_THERMAL_POLICY, (uint) performanceMode.Type, out var result);
        
        Log.Debug("Set performance mode result: {Success} {Result}", success, result);
        
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

        var fanResult = _fanController.SetCpuFanCurve(customPerformanceMode.CpuFanCurve);
        Log.Debug("Set CPU fan curve result: {Result}", fanResult);
        
        fanResult = _fanController.SetGpuFanCurve(customPerformanceMode.GpuFanCurve);
        Log.Debug("Set GPU fan curve result: {Result}", fanResult);
        
        var result = _powerLimitController.SetCpuSpl(customPerformanceMode.CpuSpl);
        Log.Debug("Set CPU SPL result: {Result}", result);
        
        result = _powerLimitController.SetCpuSppt(customPerformanceMode.CpuSppt);
        Log.Debug("Set CPU SPPT result: {Result}", result);
        
        result = _powerLimitController.SetCpuFppt(customPerformanceMode.CpuFppt);
        Log.Debug("Set CPU FPPT result: {Result}", result);
        
        _gpuControl.SetCoreClockOffset(customPerformanceMode.GpuCoreOffset);
        Log.Debug("Set memory clock offset {memoryClockOffset}", _gpuControl.GetMemoryClockOffset());
        
        _gpuControl.SetMemoryClockOffset(customPerformanceMode.GpuMemoryOffset);
        Log.Debug("Set core clock offset {coreClockOffset}", _gpuControl.GetCoreClockOffset());
    }

    private void TryResetCustomParameters(IPerformanceMode performanceMode)
    {
        if (performanceMode is not CustomPerformanceMode customPerformanceMode)
        {
            return;
        }
        
        _gpuControl.SetCoreClockOffset(0);
        _gpuControl.SetMemoryClockOffset(0);
    }
}