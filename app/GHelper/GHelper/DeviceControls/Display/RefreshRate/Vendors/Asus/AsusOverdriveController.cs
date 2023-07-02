using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Display.RefreshRate.Vendors.Asus;

public class AsusOverdriveController : IOverdriveController
{
    private readonly IAcpi _acpi;
    
    [Inject]
    public AsusOverdriveController(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void SetState(bool state)
    {
        if (!_acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_DEVID_PANEL_OD, state ? 1U : 0U, out _))
        {
            Log.Error("Failed to set overdrive state");
        }
    }
    
    public bool GetState()
    {
        var success = _acpi.TryDeviceGet((uint)AsusWmi.ASUS_WMI_DEVID_PANEL_OD, out var result);
        return success && result == 1U;
    }
}