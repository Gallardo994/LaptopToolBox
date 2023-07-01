using System.Collections.ObjectModel;
using GHelper.Helpers;
using GHelper.Updates.Models;

namespace GHelper.Updates.Core;

public interface IUpdatesProvider : IObservableObject
{
    public bool IsCheckingForUpdates { get; }
    public ObservableCollection<IUpdate> Updates { get; }
    public int PendingUpdatesCount { get; }
    public void CheckForUpdates();
}