using System.Management;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.CPU;

public partial class WmiCpuGeneralInfoProvider : ObservableObject, ICpuGeneralInfoProvider
{
    [ObservableProperty] private ICpuGeneralInfo _cpu;
    
    public WmiCpuGeneralInfoProvider()
    {
        Refresh();
    }

    public void Refresh()
    {
        using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        
        foreach (var obj in searcher.Get())
        {
            var cpuGeneralInfo = new CpuGeneralInfo
            {
                ProcessorId = obj["ProcessorId"].ToString(),
                SocketDesignation = obj["SocketDesignation"].ToString(),
                ProcessorName = obj["Name"].ToString().Trim(),
                Caption = obj["Caption"].ToString(),
                AddressWidth = ushort.Parse(obj["AddressWidth"].ToString()),
                DataWidth = ushort.Parse(obj["DataWidth"].ToString()),
                Architecture = ushort.Parse(obj["Architecture"].ToString()),
                MaxClockSpeed = uint.Parse(obj["MaxClockSpeed"].ToString()),
                ExtClock = uint.Parse(obj["ExtClock"].ToString()),
                L2CacheSize = ulong.Parse(obj["L2CacheSize"].ToString()),
                L3CacheSize = ulong.Parse(obj["L3CacheSize"].ToString()),
                NumberOfCores = uint.Parse(obj["NumberOfCores"].ToString()),
                NumberOfLogicalProcessors = uint.Parse(obj["NumberOfLogicalProcessors"].ToString()),
            };
            
            Cpu = cpuGeneralInfo;
            break;
        }
    }
}