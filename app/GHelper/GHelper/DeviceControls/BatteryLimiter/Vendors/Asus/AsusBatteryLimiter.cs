using System;
using GHelper.DeviceControls.Acpi;
using Ninject;

namespace GHelper.DeviceControls.BatteryLimiter.Vendors.Asus;

public class AsusBatteryLimiter : IBatteryLimiter
{
    private readonly IAcpi _acpi;
    
    private int _batteryLimit = 100;
    private bool _isTemporarilyUnlimited;
    
    public int MinRange => 40;
    public int MaxRange => 100;
    
    [Inject]
    public AsusBatteryLimiter(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void SetBatteryLimit(int limit)
    {
        _batteryLimit = limit;
        
        if (_isTemporarilyUnlimited)
        {
            return;
        }

        CallAcpiSetMethod(_batteryLimit);
    }
    
    public int GetBatteryLimit()
    {
        return _batteryLimit;
    }

    public bool IsTemporarilyUnlimited()
    {
        return _isTemporarilyUnlimited;
    }
    
    public void SetTemporarilyUnlimited(bool isTemporarilyUnlimited)
    {
        _isTemporarilyUnlimited = isTemporarilyUnlimited;
        CallAcpiSetMethod(isTemporarilyUnlimited ? 100 : _batteryLimit);
    }
    
    private void CallAcpiSetMethod(int value)
    {
        if (value < MinRange || value > MaxRange)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be between {MinRange} and {MaxRange}");
        }
        
        _acpi.DeviceSet(0x00120057, value);
    }
}