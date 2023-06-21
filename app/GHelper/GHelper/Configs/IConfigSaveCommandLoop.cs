namespace GHelper.Configs;

public interface IConfigSaveCommandLoop
{
    public void Enqueue(ConfigSaveCommand command);
}