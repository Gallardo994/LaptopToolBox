namespace GHelper.Updates;

public interface IUpdatesScheduler : IDisposable
{
    public void ReSchedule(TimeSpan timeSpan);
    public void CheckNow();
}