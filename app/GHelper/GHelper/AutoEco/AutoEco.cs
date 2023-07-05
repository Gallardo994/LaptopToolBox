using GHelper.Configs;
using GHelper.DeviceControls.Battery;
using GHelper.DeviceControls.GpuModes;

namespace GHelper.AutoEco;

public class AutoEco : IAutoEco
{
    private readonly IConfig _config;
    private readonly IGpuModeController _gpuModeController;
    private readonly IBatteryStateProvider _batteryStateProvider;
    
    public bool IsStarted { get; private set; }
    
    public AutoEco(IConfig config, IGpuModeController gpuModeController, IBatteryStateProvider batteryStateProvider)
    {
        _config = config;
        _gpuModeController = gpuModeController;
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
        if (!_config.AutoEcoEnabled)
        {
            return;
        }
        
        var targetMode = powerState == PowerState.OnBattery;
        _gpuModeController.SetEcoModeEnabled(targetMode);
    }
}