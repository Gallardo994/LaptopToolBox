using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using GHelper.DeviceControls.Lighting.Vendors.Asus.Aura;
using GHelper.Injection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Ninject;

namespace GHelper.ViewModels;

public partial class AuraViewModel : ObservableObject
{
    private readonly IAuraControl _auraControl;
    private readonly IAuraModesProvider _auraModesProvider;
    private readonly IAuraSpeedsProvider _auraSpeedsProvider;
    
    [ObservableProperty] private AuraModeModel _mode;
    [ObservableProperty] private Color _color;
    [ObservableProperty] private Color _color2;
    [ObservableProperty] private AuraSpeedModel _speed;
    
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
        
        // Subscribe to Mode change to call Refresh
        PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName is nameof(Mode) or nameof(Color) or nameof(Color2) or nameof(Speed))
            {
                Refresh();
            }
        };
    }

    private void Refresh()
    {
        _auraControl.Apply(Mode.Mode, Color, Color2, Speed.Speed);
    }
}