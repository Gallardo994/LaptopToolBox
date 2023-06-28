namespace GHelper.DeviceControls.PowerLimits.Vendors.Asus.Models;

public class PowerLimitInfo_G733PY : IPowerLimitRangeInfo
{
    public IPowerRange CpuSpl { get; } = new PowerRange { Default = 65, Min = 45, Max = 120 };
    public IPowerRange CpuSppt { get; } = new PowerRange { Default = 80, Min = 65, Max = 125 };
    public IPowerRange CpuFppt { get; } = new PowerRange { Default = 80, Min = 65, Max = 125 };
    
    
    public IClockRange GpuCore { get; } = new ClockRange { Default = 0, Min = 0, Max = 200 };
    public IClockRange GpuMemory { get; } = new ClockRange { Default = 0, Min = 0, Max = 300 };
    public IPowerRange GpuPower { get; } = new PowerRange { Default = 0, Min = 0, Max = 25 };
    public IThermalRange GpuTarget { get; } = new ThermalRange { Default = 75, Min = 75, Max = 87 };
}