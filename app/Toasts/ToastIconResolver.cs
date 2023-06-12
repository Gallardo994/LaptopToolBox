namespace GHelper.Toasts;

public class ToastIconResolver : IToastIconResolver
{
    public Bitmap ResolveIcon(ToastIconType toastIconType)
    {
        return toastIconType switch
        {
            ToastIconType.BrightnessUp => Properties.Resources.brightness_up,
            ToastIconType.BrightnessDown => Properties.Resources.brightness_down,
            ToastIconType.BacklightUp => Properties.Resources.backlight_up,
            ToastIconType.BacklightDown => Properties.Resources.backlight_down,
            ToastIconType.Microphone => Properties.Resources.icons8_microphone_96,
            ToastIconType.MicrophoneMute => Properties.Resources.icons8_mute_unmute_96,
            ToastIconType.Touchpad => Properties.Resources.icons8_touchpad_96,
            ToastIconType.FnLock => Properties.Resources.icons8_function,
            ToastIconType.Battery => Properties.Resources.icons8_charged_battery_96,
            ToastIconType.Charger => Properties.Resources.icons8_charging_battery_96,
            _ => throw new ArgumentOutOfRangeException(nameof(toastIconType), toastIconType, null)
        };
    }
}