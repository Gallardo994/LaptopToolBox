using GHelper.DeviceControls.HardwareMonitoring;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class CpuMonitorViewModel
{
    public readonly IHardwareReport Report = Services.ResolutionRoot.Get<IHardwareMonitor>().HardwareReport;

    public CpuMonitorViewModel()
    {
        
    }
}