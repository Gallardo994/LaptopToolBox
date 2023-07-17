using System;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.PerformanceModes;

public interface IPerformanceMode : IObservableObject
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; }
    public PerformanceModeType Type { get; }
    public bool IsAvailableOnStartup { get; set; }
    public bool IsAvailableInHotkeys { get; set; }
}