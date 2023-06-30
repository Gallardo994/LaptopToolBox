using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.GPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

namespace GHelper.DeviceControls.HardwareMonitoring;

public class HardwareReport : IHardwareReport
{
    public IRamInformation RamInformation { get; set; }
    public ICpuInformation CpuInformation { get; set; }
    public IGpuInformation GpuInformation { get; set; }
    
    public HardwareReport()
    {
        RamInformation = new RamInformation();
        CpuInformation = new CpuInformation();
        GpuInformation = new GpuInformation();
    }

    public void Clear()
    {
        RamInformation.Clear();
        CpuInformation.Clear();
        GpuInformation.Clear();
    }
}