using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;
using OpenHardwareMonitor.Hardware;
using Serilog;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class RamConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        report.RamInformation = new RamInformation();
    }
}