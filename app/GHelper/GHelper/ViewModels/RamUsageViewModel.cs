using GHelper.DeviceControls.HardwareMonitoring;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class RamUsageViewModel
{
    public readonly IHardwareReport Report = Services.ResolutionRoot.Get<IHardwareMonitor>().HardwareReport;
}