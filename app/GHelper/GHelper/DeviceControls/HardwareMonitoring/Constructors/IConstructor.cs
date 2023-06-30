using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public interface IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware);
}