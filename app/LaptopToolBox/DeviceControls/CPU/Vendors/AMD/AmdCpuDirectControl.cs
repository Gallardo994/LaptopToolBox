using System;
using Ninject;
using Serilog;

namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD;

public class AmdCpuDirectControl : ICpuDirectControl
{
    private readonly IRyzenProxy _ryzenProxy;
    private readonly ICpuFamilyProvider _cpuFamilyProvider;
    
    [Inject]
    public AmdCpuDirectControl(IRyzenProxy ryzenProxy, ICpuFamilyProvider cpuFamilyProvider)
    {
        _ryzenProxy = ryzenProxy;
        _cpuFamilyProvider = cpuFamilyProvider;
        
        Log.Debug("AMD CPU Direct Control initialized, family: {FamilyName}", _cpuFamilyProvider.FamilyName);
    }
    
    public bool IsUnderVoltSupported => _cpuFamilyProvider.FamilyId >= 0;
    
    public void SetUnderVolt(int mv)
    {
        Log.Debug("Setting undervolt to {mv}mv", mv);
        var convertedValue = Convert.ToUInt32(0x100000 - (uint)(-1 * mv));
        _ryzenProxy.set_coall(convertedValue);
    }
}