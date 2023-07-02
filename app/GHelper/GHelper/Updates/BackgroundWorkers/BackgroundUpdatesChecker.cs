using System;
using System.Timers;
using GHelper.Commands;
using GHelper.Configs;
using GHelper.Helpers;
using GHelper.Updates.Core;
using Serilog;

namespace GHelper.Updates.BackgroundWorkers;

public class BackgroundUpdatesChecker : IBackgroundUpdatesChecker
{
    private readonly IUpdatesProvider _updatesProvider;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly IConfig _config;

    private SafeTimer _timer;
    
    public BackgroundUpdatesChecker(IUpdatesProvider updatesProvider, ISTACommandLoop staCommandLoop, IConfig config)
    {
        _updatesProvider = updatesProvider;
        _staCommandLoop = staCommandLoop;
        _config = config;
    }

    public void Start()
    {
        if (!_config.DontCheckDriverUpdatesAutomatically)
        {
            _updatesProvider.CheckForUpdates();
        }
        
        _timer = new SafeTimer(TimeSpan.FromMinutes(40));
        _timer.SafeElapsed += OnTimerElapsed;
        _timer.Start();
    }
    
    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _staCommandLoop.Enqueue(() =>
        {
            if (_config.DontCheckDriverUpdatesAutomatically)
            {
                return;
            }

            _updatesProvider.CheckForUpdates();
        });
    }
}