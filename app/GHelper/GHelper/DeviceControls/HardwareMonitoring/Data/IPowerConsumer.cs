using GHelper.Helpers;

namespace GHelper.DeviceControls.HardwareMonitoring.Data;

public interface IPowerConsumer : IObservableObject
{
    public string Name { get; set; }
    public float Value { get; set; }
    public float MaxValue { get; set; }
    public int RoundedValue { get; set; }
    public int RoundedMaxValue { get; set; }

    public void Clear();
}