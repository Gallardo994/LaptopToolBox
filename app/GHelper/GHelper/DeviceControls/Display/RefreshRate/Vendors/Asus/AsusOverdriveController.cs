using GHelper.DeviceControls.Acpi;
using Ninject;

namespace GHelper.DeviceControls.Display.RefreshRate.Vendors.Asus;

public class AsusOverdriveController : IOverdriveController
{
    private readonly IAcpi _acpi;

    private const int Overdrive = 0x00050019;
    
    [Inject]
    public AsusOverdriveController(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void SetState(bool state)
    {
        _acpi.DeviceSet(Overdrive, state ? 1 : 0);
    }
    
    public bool GetState()
    {
        return _acpi.DeviceGet(Overdrive) == 1;
    }
}