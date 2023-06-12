using GHelper.Core;
using Ninject;

namespace GHelper.Settings;

public class SettingsFormController : ISettingsFormController
{
    private readonly SettingsForm _settingsForm;
    private readonly IScheduler _scheduler;
    
    [Inject]
    public SettingsFormController(SettingsForm settingsForm, IScheduler scheduler)
    {
        _settingsForm = settingsForm;
        _scheduler = scheduler;
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
                    _scheduler.ReScheduleAdmin();
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