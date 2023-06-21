using System;

namespace GHelper.DeviceControls.PerformanceModes;

public interface IPerformanceMode
{
    public Guid Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public PerformanceModeType Type { get; }
}