using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;

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
        _acpi.DeviceSet((uint) AsusWmi.ASUS_WMI_DEVID_PANEL_OD, state ? 1U : 0U);
    }
    
    public bool GetState()
    {
        return _acpi.DeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_PANEL_OD) == 1U;
    }
}