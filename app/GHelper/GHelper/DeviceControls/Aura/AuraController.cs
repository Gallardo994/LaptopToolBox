using System;
using System.Drawing;
using Ninject;

namespace GHelper.DeviceControls.Aura;

public class AuraController : IAuraController
{
    private readonly IAuraControl _auraControl;

    internal bool IsScopeActive { get; set; }

    bool IAuraController.IsScopeActive
    {
        get => IsScopeActive;
        set => IsScopeActive = value;
    }

    [Inject]
    public AuraController(IAuraControl auraControl)
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
        }
    }
    
    public AuraControllerScope Scope()
    {
        return new AuraControllerScope(this);
    }

    public bool Refresh()
    {
        if (IsScopeActive)
        {
            return false;
        }
        
        _auraControl.Apply(Mode, Color, Color2, Speed);
        return true;
    }
}