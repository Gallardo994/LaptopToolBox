namespace GHelper.Settings;

public interface ISettingsFormController : IDisposable
{
    public void Toggle(string action = "");
}