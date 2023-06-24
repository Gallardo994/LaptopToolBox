using GHelper.Helpers;

namespace GHelper.DeviceControls.CPU;

public interface ICpuGeneralInfoProvider : IObservableObject
{
    public ICpuGeneralInfo Cpu { get; }
    public void Refresh();
}