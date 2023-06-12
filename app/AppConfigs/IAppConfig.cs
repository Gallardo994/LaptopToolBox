namespace GHelper.AppConfigs;

public interface IAppConfig
{
    public AppConfigModel Model { get; }
    public void Save();
}