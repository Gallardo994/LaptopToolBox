using GHelper.DeviceControls.HardwareMonitoring;

namespace GHelper.Initializers.ConcreteInitializers;

public class HardwareMonitorInitializer : IInitializer
{
    private readonly IHardwareMonitor _hardwareMonitor;
    
    public HardwareMonitorInitializer(IHardwareMonitor hardwareMonitor)
    {
        _hardwareMonitor = hardwareMonitor;
    }
    
    public void Initialize()
    {
        _hardwareMonitor.StartMonitoring();
    }
}