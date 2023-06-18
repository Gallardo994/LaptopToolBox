using GHelper.Notifications;
using Ninject;

namespace GHelper.DeviceControls.PerformanceModes;

public class PerformanceModeControl : IPerformanceModeControl
{
    private readonly IAcpi _acpi;
    private readonly INotificationService _notificationService;

    private const uint DeviceId = 0x00120075;
    
    [Inject]
    public PerformanceModeControl(IAcpi acpi,
        INotificationService notificationService)
    {
        _acpi = acpi;
        _notificationService = notificationService;
    }

    public void SetMode(PerformanceMode performanceMode)
    {
        var result = _acpi.DeviceSet(DeviceId, (int) performanceMode.Type, "performance_mode_" + performanceMode.Type.ToString());
        
        if (result < 0)
        {
            _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Failed to set performance mode to " + performanceMode.Title);
        }
        else
        {
            _notificationService.Show(NotificationCategory.PerformanceMode, "Performance Mode", "Performance mode set to " + performanceMode.Title);
        }
    }
}