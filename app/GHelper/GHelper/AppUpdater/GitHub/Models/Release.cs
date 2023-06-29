using System;
using Newtonsoft.Json;

namespace GHelper.AppUpdater.GitHub.Models;

[JsonObject]
public class Release
{
    [JsonProperty("published_at")] public DateTime PublishedAt { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("tag_name")] public string TagName { get; set; }
    [JsonProperty("prerelease")] public bool IsPreRelease { get; set; }
    [JsonProperty("html_url")] public string HtmlUrl { get; set; }
    [JsonProperty("assets")] public Asset[] Assets { get; set; }
}