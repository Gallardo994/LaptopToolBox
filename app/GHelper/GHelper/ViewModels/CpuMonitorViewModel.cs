using System.Collections.ObjectModel;
using GHelper.DeviceControls.HardwareMonitoring;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class CpuMonitorViewModel
{
    public readonly IHardwareReport Report = Services.ResolutionRoot.Get<IHardwareMonitor>().HardwareReport;
    
    public ObservableCollection<ITemperatureSensor> Sensors => Report.CpuInformation.Sensors;

    public CpuMonitorViewModel()
    {
        
    }
}