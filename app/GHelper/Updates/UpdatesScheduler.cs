using Ninject;
using Serilog;
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
        Log.Debug("Rescheduling updates check for {TimeSpan}", timeSpan);
        
        _timer?.Stop();
        _timer?.Dispose();
        
        if (timeSpan == TimeSpan.Zero)
        {
            return;
        }
        
        _timer = new Timer(timeSpan.TotalMilliseconds);
        _timer.Elapsed += (sender, args) =>
        {
            Log.Debug("Performing automatic updates check");
            _updatesChecker.CheckForUpdates();
        };
        _timer.Start();
    }
    
    public void CheckNow()
    {
        Log.Debug("Forcing updates check");
        _updatesChecker.CheckForUpdates();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}