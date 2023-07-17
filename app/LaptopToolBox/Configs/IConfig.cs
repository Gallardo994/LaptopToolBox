using System;
using System.Collections.ObjectModel;
using LaptopToolBox.DeviceControls.PerformanceModes;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.Configs;

public interface IConfig : IObservableObject
{
    public string Path { get; }
    public bool ReadFromLocalStorage();
    public void SaveToLocalStorage();
    
    
    public bool StartMinimized { get; set; }
    public int BatteryLimit { get; set; }
    public Guid PerformanceModeCurrent { get; set; }
    public byte KeyboardBacklightBrightness { get; set; }
    public bool AutoOverdriveEnabled { get; set; }
    public ObservableCollection<CustomPerformanceMode> CustomPerformanceModes { get; set; }
    public bool DontCheckAppUpdatesAutomatically { get; set; }
    public bool DontCheckDriverUpdatesAutomatically { get; set; }
    public bool AutoEcoEnabled { get; set; }
}