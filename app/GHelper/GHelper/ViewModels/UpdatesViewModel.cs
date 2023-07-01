using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Injection;
using GHelper.Updates.Core;
using Ninject;

namespace GHelper.ViewModels;

public class UpdatesViewModel : ObservableObject
{
    public readonly IUpdatesProvider UpdatesProvider = Services.ResolutionRoot.Get<IUpdatesProvider>();

    public void CheckForUpdates()
    {
        UpdatesProvider.CheckForUpdates();
    }
}