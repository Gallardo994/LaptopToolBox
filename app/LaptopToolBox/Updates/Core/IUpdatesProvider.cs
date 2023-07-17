using System.Collections.ObjectModel;
using LaptopToolBox.Helpers;
using LaptopToolBox.Updates.Models;

namespace LaptopToolBox.Updates.Core;

public interface IUpdatesProvider : IObservableObject
{
    public bool IsCheckingForUpdates { get; }
    public ObservableCollection<IUpdate> Updates { get; }
    public int PendingUpdatesCount { get; }
    public void CheckForUpdates();
}