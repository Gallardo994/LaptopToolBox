using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.GPU;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;

namespace GHelper.DeviceControls.HardwareMonitoring;

public partial class HardwareReport : ObservableObject, IHardwareReport
{
    [ObservableProperty] private IRamInformation _ramInformation;
    [ObservableProperty] private ICpuInformation _cpuInformation;
    [ObservableProperty] private IGpuInformation _gpuInformation;
    
    [ObservableProperty] private ObservableCollection<ITemperatureSensor> _sensors;

    public HardwareReport()
    {
        RamInformation = new RamInformation();
        CpuInformation = new CpuInformation();
        GpuInformation = new GpuInformation();
        
        Sensors = new ObservableCollection<ITemperatureSensor>();
    }

    public void Clear()
    {
        RamInformation.Clear();
        CpuInformation.Clear();
        GpuInformation.Clear();
        
        foreach (var sensor in Sensors)
        {
            sensor.Clear();
        }
    }
}