using System;
using System.Linq;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.Helpers;
using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring.PostProcessors;

public class SensorsPostProcessor : IPostProcessor
{
    public void PostProcess(IHardwareReport report, IComputer computer)
    {
        var temperatureSensors = computer.Hardware
            .SelectMany(hardware => hardware.Sensors)
            .Where(sensor => sensor.SensorType == SensorType.Temperature)
            .ToList();

        ObservableCollectionHelpers.AdaptToSize(report.Sensors, temperatureSensors.Count, () => new TemperatureSensor());
        
        for (var i = 0; i < temperatureSensors.Count; i++)
        {
            report.Sensors[i].Name = temperatureSensors[i].Name;
            report.Sensors[i].Value = temperatureSensors[i].Value ?? 0;
            report.Sensors[i].RoundedValue = (int) Math.Round(temperatureSensors[i].Value ?? 0);
        }
    }
}