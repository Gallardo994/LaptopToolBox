using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

namespace GHelper.DeviceControls.HardwareMonitoring;

public interface IHardwareReport
{
    public IRamInformation RamInformation { get; set; }
    public ICpuInformation CpuInformation { get; set; }

    public void Clear();
}