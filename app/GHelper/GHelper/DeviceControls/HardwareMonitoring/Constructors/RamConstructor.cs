using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.DeviceControls.HardwareMonitoring.Data.RAM;
using OpenHardwareMonitor.Hardware;
using Serilog;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class RamConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        report.RamInformation = new RamInformation();

        foreach (var sensor in hardware.Sensors)
        {
            if (sensor.SensorType != SensorType.Data)
            {
                continue;
            }
            
            Log.Debug("Sensor: {name} ({type}) - {value} / {maxValue}", sensor.Name, sensor.SensorType, sensor.Value, sensor.Max);
        }
    }
}