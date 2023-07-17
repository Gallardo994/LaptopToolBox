using System;
using System.Timers;
using LaptopToolBox.Commands;
using LaptopToolBox.Configs;
using LaptopToolBox.Helpers;
using Serilog;

namespace LaptopToolBox.AppUpdater.BackgroundWorkers;

public class BackgroundAppUpdateChecker : IBackgroundAppUpdateChecker
{
    private readonly IAppUpdateProvider _appUpdateProvider;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly IConfig _config;

    private SafeTimer _timer;
    
    public BackgroundAppUpdateChecker(IAppUpdateProvider appUpdateProvider, ISTACommandLoop staCommandLoop, IConfig config)
    {
        _appUpdateProvider = appUpdateProvider;
        _staCommandLoop = staCommandLoop;
        _config = config;
    }

    public void Start()
    {
        if (!_config.DontCheckAppUpdatesAutomatically)
        {
            _appUpdateProvider.CheckForUpdate();
        }
        
        _timer = new SafeTimer(TimeSpan.FromMinutes(30));
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }
    
    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        _staCommandLoop.Enqueue(() =>
        {
            if (_config.DontCheckAppUpdatesAutomatically)
            {
                return;
            }

            if (_appUpdateProvider.IsUpdateInstallAvailable)
            {
                Log.Information("Update is available, but not installed. Not checking for updates.");
                return;
            }
            
            _appUpdateProvider.CheckForUpdate();
        });
    }
}