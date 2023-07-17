using LibreHardwareMonitor.Hardware;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.PostProcessors;

public interface IPostProcessor
{
    public void PostProcess(IHardwareReport report, IComputer computer);
}