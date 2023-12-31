﻿using LaptopToolBox.Toasts;
using Ninject;

namespace LaptopToolBox.Notifications;

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
            NotificationCategory.TouchPadEnable => "\uEFA5",
            NotificationCategory.TouchPadDisable => "\uEFA5",
            NotificationCategory.AppUpdateAvailable => "\uECC5",
            NotificationCategory.AppUpdateStartedDownloading => "\uEBD3",
            _ => "\uE946"
        };
    }
}