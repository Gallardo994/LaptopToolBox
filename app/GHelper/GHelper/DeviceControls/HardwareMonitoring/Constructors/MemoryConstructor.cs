using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;
using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class MemoryConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        report.RamInformation = new RamInformation();
    }
}