using LaptopToolBox.DeviceControls.HardwareMonitoring;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public class PowerConsumersViewModel
{
    public readonly IHardwareReport Report = Services.ResolutionRoot.Get<IHardwareMonitor>().HardwareReport;
}