using GHelper.Injection;
using GHelper.Updates.Core;
using Ninject;

namespace GHelper.ViewModels;

public class DeviceOutdatedWarningViewModel
{
    public IUpdatesProvider UpdatesProvider { get; } = Services.ResolutionRoot.Get<IUpdatesProvider>();
}