﻿using System.Threading;
using LaptopToolBox.Commands;
using LaptopToolBox.Notifications;

namespace LaptopToolBox.DeviceControls.Battery.Vendors.Asus;

public class AsusBatteryTemporaryUnlimitedWatcher
{
    private readonly IBattery _battery;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly INotificationService _notificationService;
    
    private Thread _thread;
    private const int CheckInterval = 1000;

    public AsusBatteryTemporaryUnlimitedWatcher(IBattery battery, ISTACommandLoop staCommandLoop, INotificationService notificationService)
    {
        _battery = battery;
        _staCommandLoop = staCommandLoop;
        _notificationService = notificationService;
    }

    public void StartWatching()
    {
        StopWatching();
        
        _thread = new Thread(ThreadRunner);
        _thread.Start();
    }

    public void StopWatching()
    {
        _thread?.Interrupt();
        _thread?.Join();
    }
    
    public bool IsRunning => _thread != null && _thread.IsAlive;
    
    private void ThreadRunner()
    {
        try
        {
            while (true)
            { 
                var currentCharge = _battery.GetCurrentCharge();

                if (currentCharge >= 100)
                {
                    _staCommandLoop.Enqueue(() =>
                    {
                        _battery.IsTemporarilyUnlimited = false;
                        _notificationService.Show(NotificationCategory.TemporaryUnlimitedBatteryChargeComplete, "Battery Charge Complete", "Unlimited battery charge has been disabled");
                    });
                    break;
                }

                if (_battery.IsCurrentlyOnBattery())
                {
                    _staCommandLoop.Enqueue(() =>
                    {
                        _battery.IsTemporarilyUnlimited = false;
                    });
                    break;
                }
                
                Thread.Sleep(CheckInterval);
            }
        }
        catch (ThreadInterruptedException)
        {
            
        }
    }
}