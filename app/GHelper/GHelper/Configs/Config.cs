using System;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Helpers;
using Newtonsoft.Json;

namespace GHelper.Configs;

[JsonObject(MemberSerialization.OptIn)]
public partial class Config : ObservableObject, IConfig
{
    private readonly IConfigSaveCommandLoop _saveCommandLoop;
    public string Path { get; init; } = System.IO.Path.Combine(ApplicationHelper.AppDataFolder, "configuration.json");
    
    [ObservableProperty] [JsonProperty("start_minimized")] private bool _startMinimized = false;
    [ObservableProperty] [JsonProperty("battery_limit")] private int _batteryLimit = 100;
    [ObservableProperty] [JsonProperty("performance_mode_current")] private Guid _performanceModeCurrent;
    [ObservableProperty] [JsonProperty("keyboard_backlight_brightness")] private byte _keyboardBacklightBrightness = 3;

    public Config(IConfigSaveCommandLoop saveCommandLoop)
    {
        _saveCommandLoop = saveCommandLoop;
        PropertyChanged += (sender, args) => SaveToLocalStorage();
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
        _saveCommandLoop.Enqueue(new ConfigSaveCommand(Path, JsonConvert.SerializeObject(this)));
    }
}