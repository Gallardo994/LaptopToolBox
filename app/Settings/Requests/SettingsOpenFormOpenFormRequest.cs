namespace GHelper.Settings.Requests;

public class SettingsOpenFormOpenFormRequest : ISettingsOpenFormRequest
{
    public event Action<string>? OpenForm;
    
    public void Invoke(string action = "")
    {
        OpenForm?.Invoke(action);
    }
    
    public void AddListener(Action<string> listener)
    {
        OpenForm += listener;
    }
    
    public void RemoveListener(Action<string> listener)
    {
        OpenForm -= listener;
    }
    
    public void Dispose()
    {
        OpenForm = null;
    }
}