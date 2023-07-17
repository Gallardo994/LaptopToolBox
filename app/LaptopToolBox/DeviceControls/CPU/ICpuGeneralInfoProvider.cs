using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.CPU;

public interface ICpuGeneralInfoProvider : IObservableObject
{
    public ICpuGeneralInfo Cpu { get; }
    public void Refresh();
}