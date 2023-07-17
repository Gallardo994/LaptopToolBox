using LaptopToolBox.Updates.Models;

namespace LaptopToolBox.Updates.IgnoredUpdates;

public interface IIgnoredUpdatesProvider
{
    public bool IsIgnored(IUpdate update);
}