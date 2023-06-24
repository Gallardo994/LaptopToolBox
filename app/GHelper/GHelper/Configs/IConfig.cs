using System;
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
}