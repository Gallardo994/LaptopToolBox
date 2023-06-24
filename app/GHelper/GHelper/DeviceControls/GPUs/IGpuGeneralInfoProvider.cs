using System.Collections.ObjectModel;
using GHelper.Helpers;

namespace GHelper.DeviceControls.GPUs;

public interface IGpuGeneralInfoProvider : IObservableObject
{
    public ObservableCollection<IGpuGeneralInfo> GpuGeneralInfoCollection { get; }
    public void Refresh();
}