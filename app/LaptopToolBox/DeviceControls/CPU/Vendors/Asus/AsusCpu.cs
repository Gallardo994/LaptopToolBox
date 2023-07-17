using Ninject;

namespace LaptopToolBox.DeviceControls.CPU.Vendors.Asus;

public class AsusCpu : ICpuControl
{
    private readonly ICpuDirectControl _cpuDirectControl;
    
    [Inject]
    public AsusCpu(ICpuDirectControl cpuDirectControl)
    {
        _cpuDirectControl = cpuDirectControl;
    }
    
    public bool IsUnderVoltSupported => _cpuDirectControl.IsUnderVoltSupported;
    public void SetUnderVolt(int mv) => _cpuDirectControl.SetUnderVolt(mv);
}