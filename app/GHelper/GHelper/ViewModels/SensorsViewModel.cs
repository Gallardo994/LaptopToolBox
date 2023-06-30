using GHelper.DeviceControls.HardwareMonitoring;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class SensorsViewModel
{
    public readonly IHardwareReport Report = Services.ResolutionRoot.Get<IHardwareMonitor>().HardwareReport;
}