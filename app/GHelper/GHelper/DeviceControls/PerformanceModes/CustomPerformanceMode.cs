using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.Fans;
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
    
    [JsonProperty("cpu_fan_curve")]
    [ObservableProperty] private FanCurve _cpuFanCurve;
    
    [JsonProperty("gpu_fan_curve")]
    [ObservableProperty] private FanCurve _gpuFanCurve;
    
    [JsonProperty("cpu_spl")]
    [ObservableProperty] private int _cpuSpl;
    
    [JsonProperty("cpu_sppt")]
    [ObservableProperty] private int _cpuSppt;
    
    [JsonProperty("cpu_fppt")]
    [ObservableProperty] private int _cpuFppt;
    
    [JsonProperty("gpu_power_boost")]
    [ObservableProperty] private int _gpuPowerBoost;
    
    [JsonProperty("gpu_temp_target")]
    [ObservableProperty] private int _gpuTempTarget;
    
    [JsonProperty("gpu_core_offset")]
    [ObservableProperty] private int _gpuCoreOffset;
    
    [JsonProperty("gpu_memory_offset")]
    [ObservableProperty] private int _gpuMemoryOffset;

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
        performanceMode.CpuFanCurve = new FanCurve(CpuFanCurve);
        performanceMode.GpuFanCurve = new FanCurve(GpuFanCurve);
        performanceMode.CpuSpl = CpuSpl;
        performanceMode.CpuSppt = CpuSppt;
        performanceMode.CpuFppt = CpuFppt;
        performanceMode.GpuPowerBoost = GpuPowerBoost;
        performanceMode.GpuTempTarget = GpuTempTarget;
        performanceMode.GpuCoreOffset = GpuCoreOffset;
        performanceMode.GpuMemoryOffset = GpuMemoryOffset;
    }
    
    public bool HasModificationsComparedTo(CustomPerformanceMode performanceMode)
    {
        return performanceMode.Id != Id ||
               performanceMode.Title != Title ||
               performanceMode.Description != Description ||
               performanceMode.IsAvailableOnStartup != IsAvailableOnStartup ||
               performanceMode.IsAvailableInHotkeys != IsAvailableInHotkeys ||
               performanceMode.CpuFanCurve.HasModificationsComparedTo(CpuFanCurve) ||
               performanceMode.GpuFanCurve.HasModificationsComparedTo(GpuFanCurve) ||
               performanceMode.CpuSpl != CpuSpl ||
               performanceMode.CpuSppt != CpuSppt ||
               performanceMode.CpuFppt != CpuFppt ||
               performanceMode.GpuPowerBoost != GpuPowerBoost ||
               performanceMode.GpuTempTarget != GpuTempTarget ||
               performanceMode.GpuCoreOffset != GpuCoreOffset ||
               performanceMode.GpuMemoryOffset != GpuMemoryOffset;
    }
}