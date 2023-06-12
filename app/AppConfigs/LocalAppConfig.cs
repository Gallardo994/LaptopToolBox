using Newtonsoft.Json;
using Ninject;

namespace GHelper.AppConfigs;

public class LocalAppConfig : IAppConfig
{
    public AppConfigModel Model { get; private set; }
    
    private IAppConfigPathProvider pathProvider;
    
    [Inject]
    public LocalAppConfig(IAppConfigPathProvider pathProvider)
    {
        var path = pathProvider.GetPath();
        var folder = Path.GetDirectoryName(pathProvider.GetPath());
        
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        
        if (File.Exists(path))
        {
            var text = File.ReadAllText(path);
            try
            {
                var model = JsonConvert.DeserializeObject<AppConfigModel>(text);
                
                if (model is null)
                {
                    InitDefault();
                }
                else
                {
                    Model = model;
                }
            }
            catch
            {
                InitDefault();
            }
        }
        else
        {
            InitDefault();
        }
    }
    
    private void InitDefault()
    {
        Model = new AppConfigModel
        {
            TopmostDisabled = false,
        };
        Save();
    }
    
    public void Save()
    {
        var path = pathProvider.GetPath();
        var folder = Path.GetDirectoryName(path);
        
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        
        var text = JsonConvert.SerializeObject(Model);
        File.WriteAllText(path, text);
    }
}