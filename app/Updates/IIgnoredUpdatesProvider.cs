namespace GHelper.Updates;

public interface IIgnoredUpdatesProvider
{
    public bool IsIgnored(IUpdate update);
}