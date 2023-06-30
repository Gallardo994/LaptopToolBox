using System;
using System.Linq;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.Helpers;
using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class GpuConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        var temperatureSensors = hardware.Sensors
            .Where(sensor => sensor.SensorType == SensorType.Temperature)
            .ToList();
        
        ObservableCollectionHelpers.AdaptToSize(report.GpuInformation.Sensors, temperatureSensors.Count, () => new TemperatureSensor());
        
        for (var i = 0; i < temperatureSensors.Count; i++)
        {
            var temperatureSensor = temperatureSensors[i];
            
            report.GpuInformation.Sensors[i].Name = temperatureSensor.Name;
            report.GpuInformation.Sensors[i].Value = temperatureSensor.Value ?? 0;
            report.GpuInformation.Sensors[i].RoundedValue = (int) Math.Round(temperatureSensor.Value ?? 0);
        }
    }
}