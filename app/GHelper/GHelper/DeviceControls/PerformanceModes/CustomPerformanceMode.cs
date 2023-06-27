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
    
    [JsonProperty("is_available_on_startup")]
    [ObservableProperty] private bool _isAvailableOnStartup;
    
    [JsonProperty("is_available_in_hotkeys")]
    [ObservableProperty] private bool _isAvailableInHotkeys;
    
    [JsonIgnore] public string Icon { get; } = "\uE7EE";
    [JsonIgnore] public PerformanceModeType Type { get; } = PerformanceModeType.Manual;

    public CustomPerformanceMode()
    {
        
    }
    
    public CustomPerformanceMode(CustomPerformanceMode performanceMode)
    {
        performanceMode.CopyTo(this);
    }
    
    public void CopyTo(CustomPerformanceMode performanceMode)
    {
        performanceMode.Id = Id;
        performanceMode.Title = Title;
        performanceMode.Description = Description;
        performanceMode.IsAvailableOnStartup = IsAvailableOnStartup;
        performanceMode.IsAvailableInHotkeys = IsAvailableInHotkeys;
    }
    
    public bool HasModificationsComparedTo(CustomPerformanceMode performanceMode)
    {
        return performanceMode.Id != Id ||
               performanceMode.Title != Title ||
               performanceMode.Description != Description ||
               performanceMode.IsAvailableOnStartup != IsAvailableOnStartup ||
               performanceMode.IsAvailableInHotkeys != IsAvailableInHotkeys;
    }
}