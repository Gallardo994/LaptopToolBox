using System.Collections.ObjectModel;
using GHelper.Helpers;

namespace GHelper.DeviceControls.GPUs;

public interface IGpuGeneralInfoProvider : IObservableObject
{
    public ObservableCollection<IGpuGeneralInfo> Collection { get; }
    public IGpuGeneralInfo BestGpu { get; }
    public void Refresh();
}