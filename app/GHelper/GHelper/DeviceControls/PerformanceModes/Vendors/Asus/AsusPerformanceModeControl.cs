using GHelper.DeviceControls.Acpi;
using GHelper.Notifications;
using Ninject;

namespace GHelper.DeviceControls.PerformanceModes.Vendors.Asus;

public class AsusPerformanceModeControl : IPerformanceModeControl
{
    private readonly IAcpi _acpi;
    private readonly INotificationService _notificationService;

    private const uint DeviceId = 0x00120075;
    
    [Inject]
    public AsusPerformanceModeControl(IAcpi acpi,
        INotificationService notificationService)
    {
        _acpi = acpi;
        _notificationService = notificationService;
    }

    public void SetMode(IPerformanceMode performanceMode)
    {
        var result = _acpi.DeviceSet(DeviceId, (int) performanceMode.Type);
        
        if (result < 0)
        {
            _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Failed to switch to " + performanceMode.Title);
        }
        else
        {
            _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Switched to " + performanceMode.Title);
        }
    }
}