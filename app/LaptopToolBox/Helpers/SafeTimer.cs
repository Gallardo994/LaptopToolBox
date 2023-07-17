using System;
using System.Timers;
using Serilog;

namespace LaptopToolBox.Helpers;

public class SafeTimer : IDisposable
{
    private readonly Timer _timer;
    public event ElapsedEventHandler Elapsed;

    public SafeTimer(double interval)
    {
        _timer = new Timer(interval);
        _timer.Elapsed += OnElapsed;
    }
    
    public SafeTimer(TimeSpan interval) : this(interval.TotalMilliseconds) { }
    
    private void OnElapsed(object sender, ElapsedEventArgs e)
    {
        try
        {
            Elapsed?.Invoke(sender, e);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "Exception occurred in SafeTimer");
            throw;
        }
    }
    
    public void Start()
    {
        _timer.Start();
    }
    
    public void Stop()
    {
        _timer.Stop();
    }

    public void Dispose()
    {
        _timer?.Dispose();
        
        Elapsed = null;
    }
}