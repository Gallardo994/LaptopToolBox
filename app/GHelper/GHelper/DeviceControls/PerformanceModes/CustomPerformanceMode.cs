using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.PerformanceModes;

[JsonObject]
public class CustomPerformanceMode : IPerformanceMode
{
    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
    [JsonIgnore] public IconElement Icon { get; set; }

    [JsonIgnore] public PerformanceModeType Type { get; } = PerformanceModeType.Manual;
}