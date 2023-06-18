using Newtonsoft.Json;

namespace GHelper.DeviceControls.PerformanceModes;

[JsonObject]
public class PerformanceMode : IPerformanceMode
{
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    [JsonProperty("type")] public PerformanceModeType Type { get; set; }
    
    [JsonIgnore] public bool IsCurrent { get; set; }
}