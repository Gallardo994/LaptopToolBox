using System.Collections.ObjectModel;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.GPUs;

public interface IGpuGeneralInfoProvider : IObservableObject
{
    public ObservableCollection<IGpuGeneralInfo> Items { get; }
    public IGpuGeneralInfo BestGpu { get; }
    public void Refresh();
}