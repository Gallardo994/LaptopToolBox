using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using Serilog;

namespace GHelper.Notifications;

// Does not work with admin rights
public class WindowsNotificationsService : INotificationService
{
    private readonly AppNotificationManager _notificationManager;
    
    public WindowsNotificationsService()
    {
        _notificationManager = AppNotificationManager.Default;
    }
    
    public void Show(NotificationCategory category, string title, string message = "")
    {
        var builder = new AppNotificationBuilder();
        
        builder.AddText(title);
        builder.AddText(message);
        
        Log.Debug("Show: {Category} {Title} {Message}", category, title, message);
        _notificationManager.Show(builder.BuildNotification());
    }
}