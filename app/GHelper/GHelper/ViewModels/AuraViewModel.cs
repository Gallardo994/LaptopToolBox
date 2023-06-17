using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using GHelper.DeviceControls.Aura;
using Ninject;

namespace GHelper.ViewModels;

public sealed class AuraViewModel : IAuraViewModel, INotifyPropertyChanged
{
    private readonly IAuraControl _auraControl;
    
    [Inject]
    public AuraViewModel(IAuraControl auraControl)
    {
        _auraControl = auraControl;
    }

    private AuraMode _mode;
    public AuraMode Mode
    {
        get => _mode;
        set 
        {
            _mode = value;
            Refresh();
            OnPropertyChanged();
        }
    }
    
    private Color _color;
    public Color Color
    {
        get => _color;
        set 
        {
            _color = value;
            Refresh();
            OnPropertyChanged();
        }
    }
    
    private Color _color2;
    public Color Color2
    {
        get => _color2;
        set 
        {
            _color2 = value;
            Refresh();
            OnPropertyChanged();
        }
    }
    
    private AuraSpeed _speed;
    public AuraSpeed Speed
    {
        get => _speed;
        set 
        {
            _speed = value;
            Refresh();
            OnPropertyChanged();
        }
    }

    private void Refresh()
    {
        _auraControl.Apply(Mode, Color, Color2, Speed);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}