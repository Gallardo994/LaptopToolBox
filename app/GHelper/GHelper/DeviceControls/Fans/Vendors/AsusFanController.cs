using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Acpi.Vendors.Asus;
using Ninject;

namespace GHelper.DeviceControls.Fans.Vendors;

public class AsusFanController : IFanController
{
    private readonly IAcpi _acpi;
    
    [Inject]
    public AsusFanController(IAcpi acpi)
    {
        _acpi = acpi;
    }
    
    public int GetCpuFanRpm()
    {
        var result = _acpi.DeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_CPU_FAN_CTRL);
        return (int) (result & (uint) AsusWmi.ASUS_WMI_DSTS_FAN_CTRL_MASK) * 100;
    }
    
    public int GetGpuFanRpm()
    {
        var result = _acpi.DeviceGet((uint) AsusWmi.ASUS_WMI_DEVID_GPU_FAN_CTRL);
        return (int) (result - (uint) AsusWmi.ASUS_WMI_DSTS_FAN_CTRL_MASK) * 100;
    }
}