using GHelper.Configs;
using GHelper.DeviceControls.Battery;
using GHelper.DeviceControls.Display.RefreshRate;
using GHelper.Notifications;

namespace GHelper.AutoOverdrive;

public class AutoOverdrive : IAutoOverdrive
{
    private readonly IConfig _config;
    private readonly IRefreshRateController _refreshRateController;
    private readonly IBatteryStateProvider _batteryStateProvider;
    private readonly INotificationService _notificationService;
    
    public bool IsStarted { get; private set; }
    
    public AutoOverdrive(IConfig config, IRefreshRateController refreshRateController, IBatteryStateProvider batteryStateProvider, INotificationService notificationService)
    {
        _config = config;
        _refreshRateController = refreshRateController;
        _batteryStateProvider = batteryStateProvider;
        _notificationService = notificationService;
    }
    
    public void Start()
    {
        if (IsStarted)
        {
            return;
        }
        
        IsStarted = true;
        
        _batteryStateProvider.PowerStateChanged += BatteryStateProviderOnPowerStateChanged;
    }
    
    public void Stop()
    {
        if (!IsStarted)
        {
            return;
        }
        
        IsStarted = false;
        
        _batteryStateProvider.PowerStateChanged -= BatteryStateProviderOnPowerStateChanged;
    }
    
    private void BatteryStateProviderOnPowerStateChanged(PowerState powerState)
    {
        if (_config.AutoOverdriveEnabled)
        {
            var targetRefreshRateMode = powerState == PowerState.OnBattery ? RefreshRateMode.Low : RefreshRateMode.High;
            
            _refreshRateController.SetMode(targetRefreshRateMode);
            
            if (powerState == PowerState.OnBattery)
            {
                _notificationService.Show(NotificationCategory.AutoOverdriveOnBattery, "Auto Overdrive", "Switched to low refresh rate mode to save battery");
            }
            else
            {
                _notificationService.Show(NotificationCategory.AutoOverdriveConnectedToPower, "Auto Overdrive", "Switched to high refresh rate mode");
            }
        }
    }
}