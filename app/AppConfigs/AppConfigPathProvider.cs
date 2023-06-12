namespace GHelper.AppConfigs;

public class AppConfigPathProvider : IAppConfigPathProvider
{
    public string GetPath()
    {
        return "Configs\\config.json";
    }
}