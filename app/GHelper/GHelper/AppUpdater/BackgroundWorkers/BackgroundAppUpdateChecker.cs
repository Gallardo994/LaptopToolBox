using System;
using System.Timers;
using GHelper.Commands;
using GHelper.Configs;
using Serilog;

namespace GHelper.AppUpdater.BackgroundWorkers;

public class BackgroundAppUpdateChecker : IBackgroundAppUpdateChecker
{
    private readonly IAppUpdateProvider _appUpdateProvider;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly IConfig _config;

    private Timer _timer;
    
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
            Log.Information("Not checking for app updates automatically");
            _appUpdateProvider.CheckForUpdate();
        }
        
        _timer = new Timer(TimeSpan.FromMinutes(30));
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