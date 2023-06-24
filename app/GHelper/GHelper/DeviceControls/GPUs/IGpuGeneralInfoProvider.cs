using GHelper.Helpers;

namespace GHelper.DeviceControls.GPUs;

public interface IGpuGeneralInfoProvider : IObservableObject
{
    public void Refresh();
}