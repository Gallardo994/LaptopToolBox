using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.PerformanceModes;

[JsonObject(MemberSerialization.OptIn)]
public partial class CustomPerformanceMode : ObservableObject, IPerformanceMode
{
    [JsonProperty("id")]
    [ObservableProperty] private Guid _id;

    [JsonProperty("title")]
    [ObservableProperty] private string _title;

    [JsonProperty("description")]
    [ObservableProperty] private string _description;
    
    [JsonIgnore] public string Icon { get; } = "\uE7EE";
    [JsonIgnore] public PerformanceModeType Type { get; } = PerformanceModeType.Manual;
}