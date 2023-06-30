using System;
using System.Collections.ObjectModel;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Helpers;

namespace GHelper.Configs;

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
}