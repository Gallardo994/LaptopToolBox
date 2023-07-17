using LaptopToolBox.AppUpdater;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public class AppOutdatedWarningViewModel
{
    public IAppUpdateProvider UpdatesProvider { get; } = Services.ResolutionRoot.Get<IAppUpdateProvider>();
}