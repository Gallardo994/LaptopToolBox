namespace GHelper.Configs;

public interface IConfig
{
    public bool ReadFromLocalStorage();
    public void SaveToLocalStorage();
}