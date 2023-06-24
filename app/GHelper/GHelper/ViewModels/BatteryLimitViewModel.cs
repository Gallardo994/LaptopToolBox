using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.Battery;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public partial class BatteryLimitViewModel : ObservableObject
{
    private readonly IBattery _battery = Services.ResolutionRoot.Get<IBattery>();
    
    [ObservableProperty] private ObservableCollection<string> _batteryLimits = new();
    
    public BatteryLimitViewModel()
    {
        for (int i = _battery.MinRange; i <= _battery.MaxRange; i += 10)
        {
            _batteryLimits.Add(i.ToString());
        }
    }
}