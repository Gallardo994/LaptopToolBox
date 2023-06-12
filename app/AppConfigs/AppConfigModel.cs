using Newtonsoft.Json;

namespace GHelper.AppConfigs;

[JsonObject]
public class AppConfigModel
{
    // General
    [JsonProperty("language")] public string Language;
    [JsonProperty("topmost_disabled")] public bool TopmostDisabled;

    // Macro keys
    [JsonProperty("m1")] public string ActionM1;
    [JsonProperty("m2")] public string ActionM2;
    [JsonProperty("m3")] public string ActionM3;
    [JsonProperty("m4")] public string ActionM4;

    // Matrix
    [JsonProperty("matrix_picture")] public string MatrixPicture;
    [JsonProperty("matrix_running")] public int MatrixRunning;
    [JsonProperty("matrix_brightness")] public int MatrixBrightness;
}