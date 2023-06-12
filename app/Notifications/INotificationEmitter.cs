namespace GHelper.Notifications;

public interface INotificationEmitter
{
    public void EmitSimple(string message);
}