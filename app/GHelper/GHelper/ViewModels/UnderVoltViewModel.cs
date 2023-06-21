using GHelper.DeviceControls.CPU;
using GHelper.Injection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Ninject;

namespace GHelper.ViewModels;

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