using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring.PostProcessors;

public interface IPostProcessor
{
    public void PostProcess(IHardwareReport report, IComputer computer);
}