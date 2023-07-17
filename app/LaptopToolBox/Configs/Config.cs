using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.PerformanceModes;
using LaptopToolBox.Helpers;
using Newtonsoft.Json;

namespace LaptopToolBox.Configs;

[JsonObject(MemberSerialization.OptIn)]
public partial class Config : ObservableObject, IConfig
{
    private readonly IConfigSaveCommandLoop _saveCommandLoop;
    public string Path { get; init; } = System.IO.Path.Combine(ApplicationHelper.AppDataFolder, "configuration.json");
    
    [ObservableProperty] [JsonProperty("start_minimized")] private bool _startMinimized = false;
    [ObservableProperty] [JsonProperty("battery_limit")] private int _batteryLimit = 100;
    [ObservableProperty] [JsonProperty("performance_mode_current")] private Guid _performanceModeCurrent;
    [ObservableProperty] [JsonProperty("keyboard_backlight_brightness")] private byte _keyboardBacklightBrightness = 3;
    [ObservableProperty] [JsonProperty("auto_overdrive_enabled")] private bool _autoOverdriveEnabled = false;
    [ObservableProperty] [JsonProperty("custom_performance_modes")] private ObservableCollection<CustomPerformanceMode> _customPerformanceModes = new();
    [ObservableProperty] [JsonProperty("dont_check_app_updates_automatically")] private bool _dontCheckAppUpdatesAutomatically = false;
    [ObservableProperty] [JsonProperty("dont_check_driver_updates_automatically")] private bool _dontCheckDriverUpdatesAutomatically = false;
    [ObservableProperty] [JsonProperty("auto_eco_enabled")] private bool _autoEcoEnabled = false;

    public Config(IConfigSaveCommandLoop saveCommandLoop)
    {
        _saveCommandLoop = saveCommandLoop;
        PropertyChanged += (sender, args) => SaveToLocalStorage();
        
        CustomPerformanceModes.CollectionChanged += (sender, args) => SaveToLocalStorage();
    }

    public bool ReadFromLocalStorage()
    {
        try
        {
            JsonConvert.PopulateObject(File.ReadAllText(Path), this);
            return true;
        }
        catch
        {
            SaveToLocalStorage(); // Overwrite the file with the default values
            return false;
        }
    }
    
    public void SaveToLocalStorage()
    {
        _saveCommandLoop.Enqueue(new ConfigSaveCommand(Path, JsonConvert.SerializeObject(this, Formatting.Indented)));
    }
}