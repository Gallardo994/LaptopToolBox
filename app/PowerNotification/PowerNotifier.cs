using GHelper.Settings;
using Ninject;

namespace GHelper.PowerNotification;

public class PowerNotifier : IPowerNotifier
{
    private readonly IntPtr _unRegPowerNotify;
    
    [Inject]
    public PowerNotifier(SettingsForm settingsForm)
    {
        var settingGuid = new NativeMethods.PowerSettingGuid();
        _unRegPowerNotify = NativeMethods.RegisterPowerSettingNotification(settingsForm.Handle, settingGuid.ConsoleDisplayState, NativeMethods.DEVICE_NOTIFY_WINDOW_HANDLE);
    }
    
    public void Dispose()
    {
        NativeMethods.UnregisterPowerSettingNotification(_unRegPowerNotify);
    }
}