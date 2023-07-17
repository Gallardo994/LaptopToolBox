using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.Commands;
using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;
using LaptopToolBox.DeviceControls.Wmi;
using LaptopToolBox.Notifications;
using Ninject;
using Serilog;

namespace LaptopToolBox.DeviceControls.Battery.Vendors.Asus;

public partial class AsusBattery : ObservableObject, IBattery
{
    private readonly IAcpi _acpi;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly INotificationService _notificationService;
    private readonly IWmiSessionFactory _wmiSessionFactory;
    
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
    public AsusBattery(IAcpi acpi, ISTACommandLoop staCommandLoop, INotificationService notificationService, IWmiSessionFactory wmiSessionFactory)
    {
        _acpi = acpi;
        _staCommandLoop = staCommandLoop;
        _notificationService = notificationService;
        _wmiSessionFactory = wmiSessionFactory;
        
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
        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.QueryInstances("root\\cimv2", "WQL", "SELECT BatteryStatus FROM Win32_Battery");
        var battery = instances.FirstOrDefault();
        return battery != null && Convert.ToInt32(battery.CimInstanceProperties["BatteryStatus"].Value) == 1;
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
        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.QueryInstances("root\\cimv2", "WQL", "SELECT EstimatedChargeRemaining FROM Win32_Battery");
        var battery = instances.FirstOrDefault();
        return battery == null ? 0 : Convert.ToInt32(battery.CimInstanceProperties["EstimatedChargeRemaining"].Value);
    }
    
    private void CallAcpiSetMethod(int value)
    {
        if (value < MinRange || value > MaxRange)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be between {MinRange} and {MaxRange}");
        }

        if (!_acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_DEVID_RSOC, (uint)value, out _))
        {
            Log.Error("Failed to set battery limit");
        }
    }
}