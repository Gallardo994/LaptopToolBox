using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using GHelper.DeviceControls.Aura;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public sealed class AuraViewModel : INotifyPropertyChanged
{
    private readonly IAuraControl _auraControl;
    private readonly IAuraModesProvider _auraModesProvider;
    private readonly IAuraSpeedsProvider _auraSpeedsProvider;
    
    public ObservableCollection<AuraModeModel> Modes => _auraModesProvider.SupportedModes;
    
    public ObservableCollection<AuraSpeedModel> Speeds => _auraSpeedsProvider.SupportedSpeeds;
    public int SpeedsMinimum => 0;
    public int SpeedsMaximum => Speeds.Count - 1;
    
    public AuraViewModel()
    {
        _auraControl = Services.ResolutionRoot.Get<IAuraControl>();
        _auraModesProvider = Services.ResolutionRoot.Get<IAuraModesProvider>();
        _auraSpeedsProvider = Services.ResolutionRoot.Get<IAuraSpeedsProvider>();
        
        _mode = Modes.First();
        _color = Color.White;
        _color2 = Color.White;
        _speed = Speeds.First();
    }

    private AuraModeModel _mode;
    public AuraModeModel Mode
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
    
    private AuraSpeedModel _speed;
    public AuraSpeedModel Speed
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
        _auraControl.Apply(Mode.Mode, Color, Color2, Speed.Speed);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}