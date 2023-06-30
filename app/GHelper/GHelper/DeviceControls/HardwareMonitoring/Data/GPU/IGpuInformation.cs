using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring.Data.GPU;

public interface IGpuInformation : ITemperatureSensorsProvider, IObservableObject
{
    public void Clear();
}