using Ninject;

namespace GHelper.Settings;

public class SettingsFormController : ISettingsFormController
{
    private SettingsForm _settingsForm;
    
    [Inject]
    public SettingsFormController(SettingsForm settingsForm)
    {
        _settingsForm = settingsForm;
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
}