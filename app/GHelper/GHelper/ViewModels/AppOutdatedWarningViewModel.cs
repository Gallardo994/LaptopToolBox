using GHelper.AppUpdater;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class AppOutdatedWarningViewModel
{
    public IAppUpdateProvider UpdatesProvider { get; } = Services.ResolutionRoot.Get<IAppUpdateProvider>();
}