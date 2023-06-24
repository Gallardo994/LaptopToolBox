using System;
using System.Linq;
using System.Management;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Commands;
using GHelper.DeviceControls.Acpi;
using GHelper.Notifications;
using Ninject;

namespace GHelper.DeviceControls.Battery.Vendors.Asus;

public partial class AsusBattery : ObservableObject, IBattery
{
    private readonly IAcpi _acpi;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly INotificationService _notificationService;
    
    private readonly AsusBatteryTemporaryUnlimitedWatcher _asusBatteryTemporaryUnlimitedWatcher;
    
    private int _batteryLimit = 100;
    private bool _isTemporarilyUnlimited;
    public bool IsTemporarilyUnlimited
    {
        get => _isTemporarilyUnlimited;
        set
        {
            SetProperty(ref _isTemporarilyUnlimited, value);
            SetTemporarilyUnlimited(value);
        }
    }

    public bool IsBatteryLimitSupported => true;
    public int MinRange => 40;
    public int MaxRange => 100;
    
    [Inject]
    public AsusBattery(IAcpi acpi, ISTACommandLoop staCommandLoop, INotificationService notificationService)
    {
        _acpi = acpi;
        _staCommandLoop = staCommandLoop;
        _notificationService = notificationService;
        
        _asusBatteryTemporaryUnlimitedWatcher = new AsusBatteryTemporaryUnlimitedWatcher(this, _staCommandLoop, _notificationService);
    }
    
    public void SetBatteryLimit(int limit)
    {
        _batteryLimit = limit;
        
        if (IsTemporarilyUnlimited)
        {
            return;
        }

        CallAcpiSetMethod(_batteryLimit);
    }
    
    public int GetBatteryLimit()
    {
        return _batteryLimit;
    }

    public bool IsCurrentlyOnBattery()
    {
        var battery = new ManagementObjectSearcher("SELECT BatteryStatus FROM Win32_Battery").Get().Cast<ManagementObject>().FirstOrDefault();
        return battery != null && Convert.ToInt32(battery["BatteryStatus"]) == 1;
    }
    
    private void SetTemporarilyUnlimited(bool isTemporarilyUnlimited)
    {
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
        var battery = new ManagementObjectSearcher("SELECT EstimatedChargeRemaining FROM Win32_Battery").Get().Cast<ManagementObject>().FirstOrDefault();
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