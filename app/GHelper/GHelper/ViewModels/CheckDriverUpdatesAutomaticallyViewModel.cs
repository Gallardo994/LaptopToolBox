using GHelper.Configs;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class CheckDriverUpdatesAutomaticallyViewModel
{
    public IConfig Config { get; } = Services.ResolutionRoot.Get<IConfig>();
}