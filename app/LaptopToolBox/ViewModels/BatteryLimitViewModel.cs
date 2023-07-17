using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.Battery;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public partial class BatteryLimitViewModel : ObservableObject
{
    public readonly IBattery Battery = Services.ResolutionRoot.Get<IBattery>();
    
    [ObservableProperty] private ObservableCollection<string> _batteryLimits = new();

    public BatteryLimitViewModel()
    {
        for (int i = Battery.MaxRange; i >= Battery.MinRange; i -= 10)
        {
            _batteryLimits.Add(i.ToString());
        }
    }
}