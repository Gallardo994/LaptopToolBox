using LaptopToolBox.DeviceControls.HardwareMonitoring;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

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