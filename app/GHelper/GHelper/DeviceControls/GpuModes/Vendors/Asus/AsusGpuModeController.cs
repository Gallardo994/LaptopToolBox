using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;

namespace GHelper.DeviceControls.GpuModes.Vendors.Asus;

public class AsusGpuModeController : IGpuModeController
{
    private readonly IAcpi _acpi;
    
    [Inject]
    public AsusGpuModeController(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public void SetEcoModeEnabled(bool enabled)
    {
        _acpi.TryDeviceSet((uint)AsusWmi.ASUS_WMI_DEVID_DGPU, enabled ? 1U : 0U, out _);
    }
}