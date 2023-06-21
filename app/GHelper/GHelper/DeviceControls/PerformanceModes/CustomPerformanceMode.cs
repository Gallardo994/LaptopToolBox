using System;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.PerformanceModes;

[JsonObject]
public class CustomPerformanceMode : IPerformanceMode
{
    [JsonProperty("id")] public Guid Id { get; set; }
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    [JsonIgnore] public string Icon { get; set; }

    [JsonIgnore] public PerformanceModeType Type { get; } = PerformanceModeType.Manual;
}