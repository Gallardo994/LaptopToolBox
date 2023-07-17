using LaptopToolBox.Configs;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public class CheckDriverUpdatesAutomaticallyViewModel
{
    public IConfig Config { get; } = Services.ResolutionRoot.Get<IConfig>();
}