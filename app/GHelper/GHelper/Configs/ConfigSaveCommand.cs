using System.IO;
using GHelper.Commands;

namespace GHelper.Configs;

public class ConfigSaveCommand : IThreadCommand
{
    private readonly string _path;
    private readonly string _content;
    
    public ConfigSaveCommand(string path, string content)
    {
        _path = path;
        _content = content;
    }

    public void Execute()
    {
        var folder = Path.GetDirectoryName(_path);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        File.WriteAllText(_path, _content);
    }
}