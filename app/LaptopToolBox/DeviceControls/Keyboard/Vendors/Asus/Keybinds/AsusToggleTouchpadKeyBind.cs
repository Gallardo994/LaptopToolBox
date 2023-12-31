﻿using LaptopToolBox.DeviceControls.TouchPad;
using LaptopToolBox.Notifications;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusToggleTouchpadKeyBind : IVendorKeyBind
{
    private readonly ITouchPadControl _touchPadControl;
    private readonly INotificationService _notificationService;
    
    public int Key { get; } = 107;
    
    public AsusToggleTouchpadKeyBind(ITouchPadControl touchPadControl, INotificationService notificationService)
    {
        _touchPadControl = touchPadControl;
        _notificationService = notificationService;
    }
    
    public void Execute()
    {
        if (!_touchPadControl.IsAvailable)
        {
            return;
        }
        
        var newState = !_touchPadControl.GetState();
        
        if (newState)
        {
            _notificationService.Show(NotificationCategory.TouchPadEnable, "TouchPad Enabled");
        }
        else
        {
            _notificationService.Show(NotificationCategory.TouchPadDisable, "TouchPad Disabled");
        }
        
        _touchPadControl.SetState(newState);
    }
}