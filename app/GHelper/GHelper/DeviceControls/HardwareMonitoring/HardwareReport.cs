using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.GPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

namespace GHelper.DeviceControls.HardwareMonitoring;

public partial class HardwareReport : ObservableObject, IHardwareReport
{
    [ObservableProperty] private IRamInformation _ramInformation;
    [ObservableProperty] private ICpuInformation _cpuInformation;
    [ObservableProperty] private IGpuInformation _gpuInformation;
    
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