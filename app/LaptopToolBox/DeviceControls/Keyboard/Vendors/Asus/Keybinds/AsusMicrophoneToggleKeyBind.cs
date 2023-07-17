using LaptopToolBox.DeviceControls.Microphone;
using LaptopToolBox.Notifications;
using Ninject;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusMicrophoneToggleKeyBind : IVendorKeyBind
{
    public int Key { get; } = 124;
    
    private readonly IMicrophoneProvider _microphoneProvider;
    private readonly INotificationService _notificationService;
    
    public AsusMicrophoneToggleKeyBind(IMicrophoneProvider microphoneProvider, INotificationService notificationService)
    {
        _microphoneProvider = microphoneProvider;
        _notificationService = notificationService;
    }
    
    public void Execute()
    {
        var targetState = !_microphoneProvider.IsMicrophoneEnabled();
        _microphoneProvider.SetState(targetState);

        if (targetState)
        {
            _notificationService.Show(NotificationCategory.MicrophoneEnable, "Microphone Enabled");
        }
        else
        {
            _notificationService.Show(NotificationCategory.MicrophoneDisable, "Microphone Disabled");
        }
    }
}