using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.CPU;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public partial class UnderVoltViewModel : ObservableObject
{
    private readonly ICpuControl _cpuControl;

    [ObservableProperty] private bool _isAvailable;
    [ObservableProperty] private int _underVolt;

    public UnderVoltViewModel()
    {
        _cpuControl = Services.ResolutionRoot.Get<ICpuControl>();
        IsAvailable = _cpuControl.IsUnderVoltSupported;
        
        PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(UnderVolt))
            {
                _cpuControl.SetUnderVolt(UnderVolt);
            }
        };
    }
}