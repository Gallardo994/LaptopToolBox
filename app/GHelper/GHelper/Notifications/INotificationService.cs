﻿namespace GHelper.Notifications;

public interface INotificationService
{
    public void Show(NotificationCategory category, string title, string message);
}