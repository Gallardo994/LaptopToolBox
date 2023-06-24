using GHelper.Toasts;
using Ninject;

namespace GHelper.Notifications;

public class ToastNotificationService : INotificationService
{
    private readonly IToastController _toastController;
    
    [Inject]
    public ToastNotificationService(IToastController toastController)
    {
        _toastController = toastController;
    }
    
    public void Show(NotificationCategory category, string title, string message = "")
    {
        _toastController.ShowToast(GetGlyphKey(category), title, message);
    }

    private string GetGlyphKey(NotificationCategory category)
    {
        return category switch
        {
            NotificationCategory.MicrophoneEnable => "\uEC71",
            NotificationCategory.MicrophoneDisable => "\uF781",
            NotificationCategory.AlwaysAwakeEnable => "\uEA3B",
            NotificationCategory.AlwaysAwakeDisable => "\uEA3A",
            _ => "\uE946"
        };
    }
}