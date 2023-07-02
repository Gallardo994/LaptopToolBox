using System;
using System.Timers;
using Serilog;

namespace GHelper.Helpers;

public class SafeTimer : Timer
{
    public event ElapsedEventHandler SafeElapsed;
    
    public SafeTimer(double interval) : base(interval)
    {
        Elapsed += OnElapsed;
    }
    
    public SafeTimer(TimeSpan interval) : base(interval.TotalMilliseconds)
    {
        Elapsed += OnElapsed;
    }
    
    private void OnElapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            SafeElapsed?.Invoke(sender, e);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "Exception occurred in SafeTimer");
            throw;
        }
    }
}