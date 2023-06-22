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
    
    public void Show(NotificationCategory category, string title, string message)
    {
        _toastController.ShowToast(title, message);
    }
}