using LaptopToolBox.Injection;
using LaptopToolBox.Updates.Core;
using Ninject;

namespace LaptopToolBox.ViewModels;

public class DeviceOutdatedWarningViewModel
{
    public IUpdatesProvider UpdatesProvider { get; } = Services.ResolutionRoot.Get<IUpdatesProvider>();
}