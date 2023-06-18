using Ninject;

namespace GHelper.DeviceControls.PerformanceModes;

public class PerformanceModeControl : IPerformanceModeControl
{
    private readonly IPerformanceModesProvider _performanceModesProvider;
    private readonly IAcpi _acpi;

    private const uint DeviceId = 0x00120075;
    
    [Inject]
    public PerformanceModeControl(
        IPerformanceModesProvider performanceModesProvider,
        IAcpi acpi)
    {
        _performanceModesProvider = performanceModesProvider;
        _acpi = acpi;

        var turbo = _performanceModesProvider.AvailableModes[0];
        SetMode(turbo);
    }

    public void SetMode(PerformanceMode performanceMode)
    {
        _acpi.DeviceSet(DeviceId, (int) performanceMode.Type, "performance_mode_" + performanceMode.Type.ToString());
    }
}