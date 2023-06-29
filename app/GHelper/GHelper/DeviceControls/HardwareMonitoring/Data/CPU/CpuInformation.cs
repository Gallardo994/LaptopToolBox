using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.CPU;

public partial class CpuInformation : ObservableObject, ICpuInformation
{
    [ObservableProperty] private int _totalLoad;
    [ObservableProperty] private ObservableCollection<ICpuCoreInformation> _coresLoad;

    public CpuInformation()
    {
        TotalLoad = 0;
        CoresLoad = new ObservableCollection<ICpuCoreInformation>();
    }
    
    public void Clear()
    {
        TotalLoad = 0;
        
        foreach (var core in CoresLoad)
        {
            core.Clear();
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
        
        return sb.ToString();
    }
}