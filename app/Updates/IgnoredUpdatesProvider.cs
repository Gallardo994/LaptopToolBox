namespace GHelper.Updates;

public class IgnoredUpdatesProvider : IIgnoredUpdatesProvider
{
    private readonly HashSet<string> _ignoredUpdates;
    
    public IgnoredUpdatesProvider()
    {
        _ignoredUpdates = new HashSet<string>
        {
            "Armoury Crate & Aura Creator Installer",
            "MyASUS",
            "ASUS Smart Display Control",
            "Aura Wallpaper",
            "Virtual Pet",
            "ROG Font V1.5"
        };
    }

    public bool IsIgnored(IUpdate update)
    {
        return _ignoredUpdates.Contains(update.Name);
    }
}