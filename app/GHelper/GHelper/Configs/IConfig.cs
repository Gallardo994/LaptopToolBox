namespace GHelper.Configs;

public interface IConfig
{
    public string Path { get; }
    public bool ReadFromLocalStorage();
    public void SaveToLocalStorage();
    
    
    public int BatteryLimit { get; set; }
}