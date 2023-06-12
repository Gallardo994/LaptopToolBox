using Microsoft.Toolkit.Uwp.Notifications;
using Serilog;

namespace GHelper.Notifications;

public class NotificationEmitter : INotificationEmitter
{
    public void EmitSimple(string message)
    {
        Log.Debug("Emitting simple notification: {Message}", message);
        
        var toastBuilder = new ToastContentBuilder()
            .AddText(message);
        
        toastBuilder.Show();
    }
}