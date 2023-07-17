using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.Commands;
using LaptopToolBox.DeviceControls.Fans;
using LaptopToolBox.Helpers;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public partial class GpuFanViewModel : ObservableObject
{
    private readonly IFanController _fanController = Services.ResolutionRoot.Get<IFanController>();
    private readonly ISTACommandLoop _commandLoop = Services.ResolutionRoot.Get<ISTACommandLoop>();

    [ObservableProperty] private int _fanRpm;

    private readonly SafeTimer _timer;
    
    public GpuFanViewModel()
    {
        _timer = new SafeTimer(1000);
        _timer.Elapsed += UpdateFanSpeeds;
        _timer.Start();
    }
    
    private void UpdateFanSpeeds(object sender, ElapsedEventArgs e)
    {
        var cpuFanRpm = _fanController.GetGpuFanRpm();
        
        _commandLoop.Enqueue(() =>
        {
            FanRpm = cpuFanRpm;
        });
    }
}