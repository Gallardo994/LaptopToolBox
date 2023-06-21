using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.DeviceControls.CPU;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class UnderVoltViewModel : INotifyPropertyChanged
{
    private readonly ICpuControl _cpuControl;

    private bool _isAvailable;
    
    public bool IsAvailable
    {
        get => _isAvailable;
        set
        {
            if (value == _isAvailable) return;
            _isAvailable = value;
            OnPropertyChanged();
        }
    }
    
    public UnderVoltViewModel()
    {
        _cpuControl = Services.ResolutionRoot.Get<ICpuControl>();
        IsAvailable = _cpuControl.IsUnderVoltSupported;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}