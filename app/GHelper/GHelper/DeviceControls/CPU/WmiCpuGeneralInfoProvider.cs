using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.Wmi;
using Ninject;

namespace GHelper.DeviceControls.CPU;

public partial class WmiCpuGeneralInfoProvider : ObservableObject, ICpuGeneralInfoProvider
{
    private readonly IWmiSessionFactory _wmiSessionFactory;
    
    [ObservableProperty] private ICpuGeneralInfo _cpu;
    
    [Inject]
    public WmiCpuGeneralInfoProvider(IWmiSessionFactory wmiSessionFactory)
    {
        _wmiSessionFactory = wmiSessionFactory;
        
        Refresh();
    }

    public void Refresh()
    {
        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.QueryInstances("root\\cimv2", "WQL", @"SELECT ProcessorId, 
                                                                            SocketDesignation,
                                                                            Name,
                                                                            Caption,
                                                                            AddressWidth,
                                                                            DataWidth,
                                                                            Architecture,
                                                                            MaxClockSpeed,
                                                                            ExtClock,
                                                                            L2CacheSize,
                                                                            L3CacheSize,
                                                                            NumberOfCores,
                                                                            NumberOfLogicalProcessors    
                                                                            FROM Win32_Processor");
        foreach (var obj in instances)
        {
            var cpuGeneralInfo = new CpuGeneralInfo
            {
                ProcessorId = obj.CimInstanceProperties["ProcessorId"].Value.ToString(),
                SocketDesignation = obj.CimInstanceProperties["SocketDesignation"].Value.ToString(),
                ProcessorName = obj.CimInstanceProperties["Name"].Value.ToString().Trim(),
                Caption = obj.CimInstanceProperties["Caption"].Value.ToString(),
                AddressWidth = ushort.Parse(obj.CimInstanceProperties["AddressWidth"].Value.ToString()),
                DataWidth = ushort.Parse(obj.CimInstanceProperties["DataWidth"].Value.ToString()),
                Architecture = ushort.Parse(obj.CimInstanceProperties["Architecture"].Value.ToString()),
                MaxClockSpeed = uint.Parse(obj.CimInstanceProperties["MaxClockSpeed"].Value.ToString()),
                ExtClock = uint.Parse(obj.CimInstanceProperties["ExtClock"].Value.ToString()),
                L2CacheSize = ulong.Parse(obj.CimInstanceProperties["L2CacheSize"].Value.ToString()),
                L3CacheSize = ulong.Parse(obj.CimInstanceProperties["L3CacheSize"].Value.ToString()),
                NumberOfCores = uint.Parse(obj.CimInstanceProperties["NumberOfCores"].Value.ToString()),
                NumberOfLogicalProcessors = uint.Parse(obj.CimInstanceProperties["NumberOfLogicalProcessors"].Value.ToString()),
            };
            
            Cpu = cpuGeneralInfo;
            break;
        }
    }
}