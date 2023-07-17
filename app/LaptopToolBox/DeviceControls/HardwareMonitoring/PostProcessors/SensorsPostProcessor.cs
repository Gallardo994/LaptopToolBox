using System;
using System.Collections.Generic;
using System.Linq;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data;
using LaptopToolBox.Helpers;
using LibreHardwareMonitor.Hardware;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.PostProcessors;

public class SensorsPostProcessor : IPostProcessor
{
    public void PostProcess(IHardwareReport report, IComputer computer)
    {
        var temperatureSensors = new List<ISensor>();
        
        foreach (var hardware in computer.Hardware)
        {
            temperatureSensors.AddRange(hardware.Sensors.Where(sensor => sensor.SensorType == SensorType.Temperature));
            temperatureSensors.AddRange(from subHardware in hardware.SubHardware from sensor in subHardware.Sensors where sensor.SensorType == SensorType.Temperature select sensor);
        }

        ObservableCollectionHelpers.AdaptToSize(report.Sensors, temperatureSensors.Count, () => new TemperatureSensor());
        
        for (var i = 0; i < temperatureSensors.Count; i++)
        {
            report.Sensors[i].Name = temperatureSensors[i].Name;
            report.Sensors[i].Value = temperatureSensors[i].Value ?? 0;
            report.Sensors[i].RoundedValue = (int) Math.Round(temperatureSensors[i].Value ?? 0);
        }
    }
}