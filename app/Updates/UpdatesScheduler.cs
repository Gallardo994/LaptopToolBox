using Ninject;
using Timer = System.Timers.Timer;

namespace GHelper.Updates;

public class UpdatesScheduler : IUpdatesScheduler
{
    private readonly IUpdatesChecker _updatesChecker;
    private Timer? _timer;
    
    [Inject]
    public UpdatesScheduler(IUpdatesChecker updatesChecker)
    {
        _updatesChecker = updatesChecker;
    }
    
    public void ReSchedule(TimeSpan timeSpan)
    {
        _timer?.Stop();
        _timer?.Dispose();
        
        _timer = new Timer(timeSpan.TotalMilliseconds);
        _timer.Elapsed += (sender, args) => _updatesChecker.CheckForUpdates();
        _timer.Start();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}