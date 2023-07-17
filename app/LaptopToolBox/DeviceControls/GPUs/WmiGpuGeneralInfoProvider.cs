using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.Wmi;
using Microsoft.Management.Infrastructure;
using Ninject;

namespace LaptopToolBox.DeviceControls.GPUs;

public partial class WmiGpuGeneralInfoProvider : ObservableObject, IGpuGeneralInfoProvider
{
    private readonly IWmiSessionFactory _wmiSessionFactory;
    
    [ObservableProperty] private ObservableCollection<IGpuGeneralInfo> _items;
    [ObservableProperty] private IGpuGeneralInfo _bestGpu;

    [Inject]
    public WmiGpuGeneralInfoProvider(IWmiSessionFactory wmiSessionFactory)
    {
        _wmiSessionFactory = wmiSessionFactory;
        Refresh();
    }

    public void Refresh()
    {
        var items = new ObservableCollection<IGpuGeneralInfo>();
        
        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.QueryInstances("root\\cimv2", "WQL", @"SELECT Name, 
                                                                            DeviceID,
                                                                            AdapterRAM,
                                                                            AdapterDACType,
                                                                            Monochrome,
                                                                            InstalledDisplayDrivers,
                                                                            DriverVersion,
                                                                            VideoProcessor,
                                                                            VideoArchitecture,
                                                                            VideoMemoryType
                                                                            FROM Win32_VideoController");
        
        foreach (var obj in instances)
        {
            var gpuGeneralInfo = new GpuGeneralInfo
            {
                DeviceName = obj.CimInstanceProperties["Name"].Value.ToString(),
                DeviceId = obj.CimInstanceProperties["DeviceID"].Value.ToString(),
                AdapterRam = uint.Parse(obj.CimInstanceProperties["AdapterRAM"].Value.ToString()),
                AdapterDacType = obj.CimInstanceProperties["AdapterDACType"].Value.ToString(),
                Monochrome = obj.CimInstanceProperties["Monochrome"].Value.ToString() == "True",
                InstalledDisplayDrivers = obj.CimInstanceProperties["InstalledDisplayDrivers"].Value.ToString(),
                DriverVersion = obj.CimInstanceProperties["DriverVersion"].Value.ToString(),
                VideoProcessor = obj.CimInstanceProperties["VideoProcessor"].Value.ToString(),
                VideoArchitecture = ushort.Parse(obj.CimInstanceProperties["VideoArchitecture"].Value.ToString()),
                VideoMemoryType = ushort.Parse(obj.CimInstanceProperties["VideoMemoryType"].Value.ToString()),
            };
            
            items.Add(gpuGeneralInfo);
        }
        
        Items = items;
        BestGpu = Items.OrderByDescending(gpu => gpu.AdapterRam).First();
    }
}
