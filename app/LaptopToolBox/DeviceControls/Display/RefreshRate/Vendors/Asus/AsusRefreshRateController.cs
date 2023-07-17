using System;
using System.Linq;
using LaptopToolBox.DeviceControls.Display.Screens;
using Ninject;

namespace LaptopToolBox.DeviceControls.Display.RefreshRate.Vendors.Asus;

public class AsusRefreshRateController : IRefreshRateController
{
    private readonly IOverdriveController _overdrive;
    private readonly IScreenProvider _screenProvider;

    [Inject]
    public AsusRefreshRateController(IOverdriveController overdrive, IScreenProvider screenProvider)
    {
        _overdrive = overdrive;
        _screenProvider = screenProvider;
    }

    public void SetMode(RefreshRateMode mode)
    {
        _screenProvider.Refresh();
        
        switch (mode)
        {
            case RefreshRateMode.Low:
                _overdrive.SetState(false);
                SetMinimalRefreshRate();
                break;
            case RefreshRateMode.High:
                _overdrive.SetState(true);
                SetMaximalRefreshRate();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private void SetMinimalRefreshRate()
    {
        var screen = _screenProvider.Items.FirstOrDefault();
        
        if (screen == null)
        {
            return;
        }
        
        screen.SetRefreshRate(screen.GetSupportedRefreshRates().First());
    }
    
    private void SetMaximalRefreshRate()
    {
        var screen = _screenProvider.Items.FirstOrDefault();
        
        if (screen == null)
        {
            return;
        }
        
        screen.SetRefreshRate(screen.GetSupportedRefreshRates().Last());
    }
    
    public RefreshRateMode GetMode()
    {
        return _overdrive.GetState() ? RefreshRateMode.High : RefreshRateMode.Low; // TODO: Only watches overdrive rn. Need to support real refresh rate.
    }
}