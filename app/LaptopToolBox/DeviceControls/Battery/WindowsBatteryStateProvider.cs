using System;
using System.Timers;
using LaptopToolBox.Commands;
using LaptopToolBox.Helpers;
using Ninject;

namespace LaptopToolBox.DeviceControls.Battery;

public class WindowsBatteryStateProvider : IBatteryStateProvider
{
    public event Action<PowerState> PowerStateChanged;
    public PowerState CurrentPowerState => _powerState;
    
    private readonly IBattery _battery;
    private readonly ISTACommandLoop _staCommandLoop;
    
    private PowerState _powerState;
    private readonly SafeTimer _timer;

    [Inject]
    public WindowsBatteryStateProvider(IBattery battery, ISTACommandLoop staCommandLoop)
    {
        _battery = battery;
        _staCommandLoop = staCommandLoop;
        
        _timer = new SafeTimer(500);
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }
    
    private void TimerOnElapsed(object sender, ElapsedEventArgs e)
    {
        var currentPowerState = GetCurrentPowerState();
        
        if (currentPowerState != _powerState)
        {
            _staCommandLoop.Enqueue(() =>
            {
                _powerState = currentPowerState;
                PowerStateChanged?.Invoke(_powerState);
            });
        }
    }
    
    private PowerState GetCurrentPowerState()
    {
        if (_battery.IsCurrentlyOnBattery())
        {
            return PowerState.OnBattery;
        }
        
        return _battery.GetCurrentCharge() == 0 ? PowerState.NoBattery : PowerState.Charging;
    }
}