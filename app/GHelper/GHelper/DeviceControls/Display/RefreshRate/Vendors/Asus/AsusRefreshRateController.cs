using System;
using Ninject;

namespace GHelper.DeviceControls.Display.RefreshRate.Vendors.Asus;

public class AsusRefreshRateController : IRefreshRateController
{
    private readonly IOverdriveController _overdrive;

    [Inject]
    public AsusRefreshRateController(IOverdriveController overdrive)
    {
        _overdrive = overdrive;
    }
    
    public void SetMode(RefreshRateMode mode)
    {
        switch (mode)
        {
            case RefreshRateMode.Low:
                _overdrive.SetState(false);
                break;
            case RefreshRateMode.High:
                _overdrive.SetState(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }
    
    public RefreshRateMode GetMode()
    {
        return _overdrive.GetState() ? RefreshRateMode.High : RefreshRateMode.Low; // TODO: Only watches overdrive rn. Need to support real refresh rate.
    }
}