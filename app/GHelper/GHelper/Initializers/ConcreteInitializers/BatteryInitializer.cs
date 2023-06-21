using GHelper.Configs;
using GHelper.DeviceControls.Battery;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class BatteryInitializer : IInitializer
{
    private readonly IConfig _config;
    private readonly IBattery _battery;
    
    [Inject]
    public BatteryInitializer(IConfig config, IBattery battery)
    {
        _config = config;
        _battery = battery;
    }
    
    public void Initialize()
    {
        var limit = _config.BatteryLimit;
        
        if (limit < _battery.MinRange || limit > _battery.MaxRange)
        {
            limit = _battery.MaxRange;
        }
        
        _battery.SetBatteryLimit(limit);
    }
}