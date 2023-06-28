namespace GHelper.DeviceControls.PowerLimits.Vendors.Asus.Models;

public class PowerLimitInfo_Default : IPowerLimitRangeInfo
{
    public IPowerRange CpuSpl { get; } = new PowerRange { Default = 45, Min = 30, Max = 100 };
    public IPowerRange CpuSppt { get; } = new PowerRange { Default = 45, Min = 30, Max = 100 };
    public IPowerRange CpuFppt { get; } = new PowerRange { Default = 45, Min = 30, Max = 100 };
    
    
    public IClockRange GpuCore { get; } = new ClockRange { Default = 0, Min = 0, Max = 200 };
    public IClockRange GpuMemory { get; } = new ClockRange { Default = 0, Min = 0, Max = 300 };
    public IPowerRange GpuPower { get; } = new PowerRange { Default = 0, Min = 0, Max = 25 };
    public IThermalRange GpuTarget { get; } = new ThermalRange { Default = 75, Min = 75, Max = 87 };
}