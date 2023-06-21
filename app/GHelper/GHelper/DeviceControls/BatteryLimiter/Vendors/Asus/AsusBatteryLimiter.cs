using GHelper.DeviceControls.Acpi;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.BatteryLimiter.Vendors.Asus;

public class AsusBatteryLimiter : IBatteryLimiter
{
    private readonly IAcpi _acpi;
    
    private int _batteryLimit = 100;
    
    [Inject]
    public AsusBatteryLimiter(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void SetBatteryLimit(int limit)
    {
        if (_acpi.DeviceSet(0x00120057, limit) > 0)
        {
            _batteryLimit = limit;
            Log.Debug("Setting battery limit to {limit}", limit);
        }
        else
        {
            Log.Debug("Failed to set battery limit to {limit}", limit);
        }
    }
    
    public int GetBatteryLimit()
    {
        return _batteryLimit;
    }
}