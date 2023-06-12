namespace GHelper.Toasts;

public class ToastIconResolver : IToastIconResolver
{
    public Bitmap ResolveIcon(ToastIcon toastIcon)
    {
        return toastIcon switch
        {
            ToastIcon.BrightnessUp => Properties.Resources.brightness_up,
            ToastIcon.BrightnessDown => Properties.Resources.brightness_down,
            ToastIcon.BacklightUp => Properties.Resources.backlight_up,
            ToastIcon.BacklightDown => Properties.Resources.backlight_down,
            ToastIcon.Microphone => Properties.Resources.icons8_microphone_96,
            ToastIcon.MicrophoneMute => Properties.Resources.icons8_mute_unmute_96,
            ToastIcon.Touchpad => Properties.Resources.icons8_touchpad_96,
            ToastIcon.FnLock => Properties.Resources.icons8_function,
            ToastIcon.Battery => Properties.Resources.icons8_charged_battery_96,
            ToastIcon.Charger => Properties.Resources.icons8_charging_battery_96,
            _ => throw new ArgumentOutOfRangeException(nameof(toastIcon), toastIcon, null)
        };
    }
}