using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Commands;
using GHelper.Notifications;
using GHelper.Updates.Commands;
using GHelper.Updates.Models;
using Ninject;
using Serilog;

namespace GHelper.Updates.Core;

public partial class UpdatesProvider : ObservableObject, IUpdatesProvider
{
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly IUpdatesCommandLoop _updatesCommandLoop;
    private readonly IUpdatesChecker _updatesChecker;
    private readonly INotificationService _notificationService;

    [ObservableProperty] private bool _isCheckingForUpdates;
    [ObservableProperty] private ObservableCollection<IUpdate> _updates;
    [ObservableProperty] private int _pendingUpdatesCount;

    [Inject]
    public UpdatesProvider(ISTACommandLoop staCommandLoop, IUpdatesCommandLoop updatesCommandLoop, IUpdatesChecker updatesChecker, INotificationService notificationService)
    {
        _staCommandLoop = staCommandLoop;
        _updatesCommandLoop = updatesCommandLoop;
        _updatesChecker = updatesChecker;
        _notificationService = notificationService;

        IsCheckingForUpdates = false;
        Updates = new ObservableCollection<IUpdate>();
        PendingUpdatesCount = 0;
    }

    public void CheckForUpdates()
    {
        if (IsCheckingForUpdates)
        {
            Log.Information("Driver & Bios Update checking is already in progress");
            return;
        }

        Updates.Clear();
        IsCheckingForUpdates = true;
        PendingUpdatesCount = 0;

        _updatesCommandLoop.Enqueue(() =>
        {
            var updatesTask = _updatesChecker.CheckForUpdates();
            updatesTask.Wait();

            _staCommandLoop.Enqueue(() =>
            {
                IsCheckingForUpdates = false;

                if (updatesTask.Result == null)
                {
                    Log.Information("Driver & Bios Update check failed");
                    return;
                }

                Updates.Clear();
                foreach (var update in updatesTask.Result)
                {
                    Updates.Add(update);
                }
                
                PendingUpdatesCount = updatesTask.Result.Count(update => update.IsNewerThanCurrent);
                
                if (PendingUpdatesCount == 0)
                {
                    Log.Information("No new updates found");
                    return;
                }
                
                _notificationService.Show(NotificationCategory.DriverUpdatesAvailable, $"New Drivers updates are available", $"New updates count: {PendingUpdatesCount}");
            });
        });
    }
}