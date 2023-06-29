using OpenHardwareMonitor.Hardware;
using Serilog;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class MainboardConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        foreach (var sensor in hardware.Sensors)
        {
            Log.Debug("Mainboard Sensor: {name} ({type}) - {value} / {maxValue}", sensor.Name, sensor.SensorType, sensor.Value, sensor.Max);
        }
    }
}