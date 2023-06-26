using GHelper.Configs;
using GHelper.DeviceControls.Battery;
using Ninject;
using Serilog;

namespace GHelper.Initializers.ConcreteInitializers;

public class BatteryInitializer : IInitializer
{
    private readonly IConfig _config;
    private readonly IBattery _battery;
    private readonly IBatteryStateProvider _batteryStateProvider;
    
    [Inject]
    public BatteryInitializer(IConfig config, IBattery battery, IBatteryStateProvider batteryStateProvider)
    {
        _config = config;
        _battery = battery;
        _batteryStateProvider = batteryStateProvider;
    }
    
    public void Initialize()
    {
        var limit = _config.BatteryLimit;
        
        if (limit < _battery.MinRange || limit > _battery.MaxRange)
        {
            limit = _battery.MaxRange;
        }
        
        _battery.SetBatteryLimit(limit);
        
        _batteryStateProvider.PowerStateChanged += OnPowerStateChanged;
    }
    
    private void OnPowerStateChanged(PowerState powerState)
    {
        Log.Information("Power state changed to {PowerState}", powerState);
    }
}