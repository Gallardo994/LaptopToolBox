using System.IO;
using GHelper.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GHelper.Configs;

[JsonObject(MemberSerialization.OptIn)]
public partial class Config : ObservableObject, IConfig
{
    private static string Path => System.IO.Path.Combine(ApplicationHelper.AppDataFolder, "configuration.json");
    
    //[ObservableProperty] [JsonProperty("autostart")] private bool _autoStart = false;
    //[ObservableProperty] [JsonProperty("battery_limit")] private int _batteryLimit = 100;

    public Config()
    {
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
        var folder = System.IO.Path.GetDirectoryName(Path);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        File.WriteAllText(Path, JsonConvert.SerializeObject(this));
    }
}