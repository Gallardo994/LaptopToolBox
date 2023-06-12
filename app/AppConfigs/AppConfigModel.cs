using Newtonsoft.Json;

namespace GHelper.AppConfigs;

[JsonObject]
public class AppConfigModel
{
    [JsonProperty("topmost_disabled")] public bool TopmostDisabled;
}