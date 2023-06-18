using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

namespace GHelper.Notifications;

public class WindowsNotificationsService : INotificationService
{
    private readonly AppNotificationManager _notificationManager;
    
    public WindowsNotificationsService()
    {
        _notificationManager = AppNotificationManager.Default;
    }
    
    public void Show(NotificationCategory category, string title, string message)
    {
        var builder = new AppNotificationBuilder();
        
        builder.AddText(title);
        builder.AddText(message);
        
        _notificationManager.Show(builder.BuildNotification());
    }
}