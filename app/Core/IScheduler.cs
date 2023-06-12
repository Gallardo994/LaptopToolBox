namespace GHelper.Core;

public interface IScheduler
{
    public bool IsScheduled();
    public void ReScheduleAdmin();
}