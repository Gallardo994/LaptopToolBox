using LaptopToolBox.AlwaysAwake;
using LaptopToolBox.Notifications;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusAlwaysAwakeKeyBind : IVendorKeyBind
{
    private readonly IAlwaysAwakeController _alwaysAwakeController;
    private readonly INotificationService _notificationService;
    
    public int Key { get; set; } = 158;
    

    public AsusAlwaysAwakeKeyBind(IAlwaysAwakeController alwaysAwakeController, INotificationService notificationService)
    {
        _alwaysAwakeController = alwaysAwakeController;
        _notificationService = notificationService;
    }

    public void Execute()
    {
        if (_alwaysAwakeController.IsRunning)
        {
            _alwaysAwakeController.Stop();
            _notificationService.Show(NotificationCategory.AlwaysAwakeDisable, "Always Awake Disabled", "Your computer will now sleep normally");

        }
        else
        {
            _alwaysAwakeController.Start();
            _notificationService.Show(NotificationCategory.AlwaysAwakeEnable, "Always Awake Enabled", "Your computer will no longer sleep automatically");
        }
    }
}