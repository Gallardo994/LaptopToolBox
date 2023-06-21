using System;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.CPU.Vendors.AMD;

public class AmdCpuDirectControl : ICpuDirectControl
{
    private readonly IRyzenProxy _ryzenProxy;
    private readonly IAmdFamilyProvider _amdFamilyProvider;
    
    [Inject]
    public AmdCpuDirectControl(IRyzenProxy ryzenProxy, IAmdFamilyProvider amdFamilyProvider)
    {
        _ryzenProxy = ryzenProxy;
        _amdFamilyProvider = amdFamilyProvider;
        
        Log.Debug("AMD CPU Direct Control initialized, family: {FamilyName}", _amdFamilyProvider.FamilyName);
    }
    
    public bool IsUnderVoltSupported => _amdFamilyProvider.FamilyId >= 0;
    
    public void SetUnderVolt(int mv)
    {
        Log.Debug("Setting undervolt to {mv}mv", mv);
        var convertedValue = Convert.ToUInt32(0x100000 - (uint)(-1 * mv));
        _ryzenProxy.set_coall(convertedValue);
    }
}