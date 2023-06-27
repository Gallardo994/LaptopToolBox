using System;
using GHelper.Helpers;

namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceMode : IObservableObject
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; }
    public PerformanceModeType Type { get; }
}