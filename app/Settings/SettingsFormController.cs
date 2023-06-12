using GHelper.Settings.Requests;
using Ninject;

namespace GHelper.Settings;

public class SettingsFormController : ISettingsFormController
{
    private readonly ISettingsOpenFormRequest _settingsOpenFormRequest;
    private readonly SettingsForm _settingsForm;
    
    [Inject]
    public SettingsFormController(ISettingsOpenFormRequest settingsOpenFormRequest, SettingsForm settingsForm)
    {
        _settingsOpenFormRequest = settingsOpenFormRequest;
        _settingsForm = settingsForm;

        _settingsOpenFormRequest.AddListener(Toggle);
    }
    
    public void Toggle(string action = "")
    {
        if (_settingsForm.Visible)
        {
            _settingsForm.HideAll();
        }
        else
        {
            _settingsForm.Show();
            _settingsForm.Activate();
            _settingsForm.VisualiseGPUMode();

            switch (action)
            {
                case "gpu":
                    Startup.ReScheduleAdmin();
                    _settingsForm.FansToggle();
                    break;
                case "gpurestart":
                    _settingsForm.RestartGPU(false);
                    break;
                case "services":
                    _settingsForm.keyb = new Extra();
                    _settingsForm.keyb.Show();
                    _settingsForm.keyb.ServiesToggle();
                    break;
            }
        }
    }
    
    public void Dispose()
    {
        _settingsOpenFormRequest.RemoveListener(Toggle);
    }
}