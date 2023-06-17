using GHelper.Updates.Models;

namespace GHelper.Updates.IgnoredUpdates;

public interface IIgnoredUpdatesProvider
{
    public bool IsIgnored(IUpdate update);
}