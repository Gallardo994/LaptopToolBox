using System;
using System.Linq;
using System.Management;
using GHelper.Commands;
using GHelper.DeviceControls.Acpi;
using Ninject;

namespace GHelper.DeviceControls.Battery.Vendors.Asus;

public class AsusBattery : IBattery
{
    private readonly IAcpi _acpi;
    private readonly ISTACommandLoop _staCommandLoop;
    
    private readonly AsusBatteryTemporaryUnlimitedWatcher _asusBatteryTemporaryUnlimitedWatcher;
    
    private int _batteryLimit = 100;
    private bool _isTemporarilyUnlimited;

    public bool IsBatteryLimitSupported => true;
    public int MinRange => 40;
    public int MaxRange => 100;
    
    [Inject]
    public AsusBattery(IAcpi acpi, ISTACommandLoop staCommandLoop)
    {
        _acpi = acpi;
        _staCommandLoop = staCommandLoop;
        
        _asusBatteryTemporaryUnlimitedWatcher = new AsusBatteryTemporaryUnlimitedWatcher(this, _staCommandLoop);
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
    
    public bool IsCurrentlyOnBattery()
    {
        var battery = new ManagementObjectSearcher("SELECT * FROM Win32_Battery").Get().Cast<ManagementObject>().FirstOrDefault();
        return battery != null && Convert.ToBoolean(battery["BatteryStatus"]);
    }
    
    public void SetTemporarilyUnlimited(bool isTemporarilyUnlimited)
    {
        _isTemporarilyUnlimited = isTemporarilyUnlimited;
        CallAcpiSetMethod(isTemporarilyUnlimited ? 100 : _batteryLimit);
        if (isTemporarilyUnlimited)
        {
            _asusBatteryTemporaryUnlimitedWatcher.StartWatching();
        }
        else
        {
            _asusBatteryTemporaryUnlimitedWatcher.StopWatching();
        }
    }

    public int GetCurrentCharge()
    {
        var battery = new ManagementObjectSearcher("SELECT * FROM Win32_Battery").Get().Cast<ManagementObject>().FirstOrDefault();
        return battery == null ? 0 : Convert.ToInt32(battery["EstimatedChargeRemaining"]);
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