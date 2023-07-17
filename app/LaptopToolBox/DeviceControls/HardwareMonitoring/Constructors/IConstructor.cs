using LibreHardwareMonitor.Hardware;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Constructors;

public interface IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware);
}