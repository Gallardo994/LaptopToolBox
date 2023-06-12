namespace GHelper.Settings.Requests;

public interface ISettingsOpenFormRequest : IDisposable
{
    public void Invoke(string action = "");
    public void AddListener(Action<string> listener);
    public void RemoveListener(Action<string> listener);
}