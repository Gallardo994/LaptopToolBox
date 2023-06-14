namespace GHelper.Updates;

public interface IUpdatesChecker
{
    public Task<List<IUpdate>> CheckForUpdates();
}