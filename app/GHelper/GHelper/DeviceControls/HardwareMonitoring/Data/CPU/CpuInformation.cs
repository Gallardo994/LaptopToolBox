using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public partial class CpuInformation : ObservableObject, ICpuInformation
{
    [ObservableProperty] private int _totalLoad;
    [ObservableProperty] private ObservableCollection<ICpuCoreInformation> _coresLoad;
    [ObservableProperty] private ObservableCollection<ITemperatureSensor> _sensors;

    public CpuInformation()
    {
        TotalLoad = 0;
        CoresLoad = new ObservableCollection<ICpuCoreInformation>();
        Sensors = new ObservableCollection<ITemperatureSensor>();
    }
    
    public void Clear()
    {
        TotalLoad = 0;
        
        foreach (var core in CoresLoad)
        {
            core.Clear();
        }
        
        foreach (var sensor in Sensors)
        {
            sensor.Clear();
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"Total Load: {TotalLoad}");
        
        sb.AppendLine("Cores Load:");
        foreach (var core in CoresLoad)
        {
            sb.AppendLine(core.ToString());
        }
        
        sb.AppendLine("Sensors:");
        foreach (var sensor in Sensors)
        {
            sb.AppendLine(sensor.ToString());
        }
        
        return sb.ToString();
    }
}