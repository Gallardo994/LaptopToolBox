using System.Collections.Generic;

namespace GHelper.Updates;

public interface IUpdatesChecker
{
    public bool IsCheckingForUpdates { get; }
    public List<IUpdate> AllUpdates { get; }
    public int PendingUpdatesCount { get; }
    public bool CheckForUpdates();
}