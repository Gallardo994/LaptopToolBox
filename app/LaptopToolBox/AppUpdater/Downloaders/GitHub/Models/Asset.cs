using Newtonsoft.Json;

namespace LaptopToolBox.AppUpdater.Downloaders.GitHub.Models;

[JsonObject]
public class Asset
{
    [JsonProperty("content_type")] public string ContentType { get; set; }
    [JsonProperty("browser_download_url")] public string DownloadUrl { get; set; }
    [JsonProperty("size")] public long Size { get; set; }
}