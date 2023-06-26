using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Commands;
using GHelper.DeviceControls.Fans;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public partial class FansViewModel : ObservableObject
{
    private readonly IFanController _fanController = Services.ResolutionRoot.Get<IFanController>();
    private readonly ISTACommandLoop _commandLoop = Services.ResolutionRoot.Get<ISTACommandLoop>();

    [ObservableProperty] private int _cpuFanRpm;
    [ObservableProperty] private int _gpuFanRpm;

    private readonly Timer _timer;
    
    public FansViewModel()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += UpdateFanSpeeds;
        _timer.Start();
    }
    
    private void UpdateFanSpeeds(object sender, ElapsedEventArgs e)
    {
        var cpuFanRpm = _fanController.GetCpuFanRpm();
        var gpuFanRpm = _fanController.GetGpuFanRpm();
        
        _commandLoop.Enqueue(() =>
        {
            CpuFanRpm = cpuFanRpm;
            GpuFanRpm = gpuFanRpm;
        });
    }
}