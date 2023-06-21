using System;

namespace GHelper.DeviceControls.PerformanceModes;

public class IntegratedPerformanceMode : IPerformanceMode
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public PerformanceModeType Type { get; init; }
}