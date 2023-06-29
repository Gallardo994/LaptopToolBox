using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

namespace GHelper.DeviceControls.HardwareMonitoring;

public class HardwareReport : IHardwareReport
{
    public IRamInformation RamInformation { get; set; }
    public ICpuInformation CpuInformation { get; set; }

    public HardwareReport()
    {
        RamInformation = new RamInformation();
        CpuInformation = new CpuInformation();
    }

    public void Clear()
    {
        RamInformation.Clear();
        CpuInformation.Clear();
    }
}