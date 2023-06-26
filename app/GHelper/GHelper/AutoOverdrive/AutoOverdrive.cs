using GHelper.Configs;
using GHelper.DeviceControls.Battery;
using GHelper.DeviceControls.Display.RefreshRate;

namespace GHelper.AutoOverdrive;

public class AutoOverdrive : IAutoOverdrive
{
    private readonly IConfig _config;
    private readonly IRefreshRateController _refreshRateController;
    private readonly IBatteryStateProvider _batteryStateProvider;
    
    public bool IsStarted { get; private set; }
    
    public AutoOverdrive(IConfig config, IRefreshRateController refreshRateController, IBatteryStateProvider batteryStateProvider)
    {
        _config = config;
        _refreshRateController = refreshRateController;
        _batteryStateProvider = batteryStateProvider;
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
            _refreshRateController.SetMode(powerState == PowerState.OnBattery ? RefreshRateMode.Low : RefreshRateMode.High);
        }
    }
}