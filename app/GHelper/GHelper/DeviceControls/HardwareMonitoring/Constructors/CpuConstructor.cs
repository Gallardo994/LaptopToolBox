using System;
using System.Linq;
using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.Helpers;
using LibreHardwareMonitor.Hardware;
using Serilog;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class CpuConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        var total = hardware.Sensors.FirstOrDefault(sensor => sensor.SensorType == SensorType.Load && sensor.Name.Contains("Total"));
        report.CpuInformation.TotalLoad = (int) (total?.Value ?? 0);
        
        var cores = hardware.Sensors
            .Where(sensor => sensor.SensorType == SensorType.Load && sensor.Name.Contains("Core"))
            .OrderBy(sensor => int.Parse(sensor.Name.AsSpan()[(sensor.Name.IndexOf('#') + 1)..]))
            .ToList();

        ObservableCollectionHelpers.AdaptToSize(report.CpuInformation.CoresLoad, cores.Count, () => new CpuCoreInformation());

        for (var i = 0; i < cores.Count; i++)
        {
            var coreSensor = cores[i];
            
            report.CpuInformation.CoresLoad[i].Name = coreSensor.Name;
            report.CpuInformation.CoresLoad[i].CoreIndex = i;
            report.CpuInformation.CoresLoad[i].CoreNumber = i + 1;
            report.CpuInformation.CoresLoad[i].TotalLoad = (int) Math.Round(coreSensor.Value ?? 0);
        }
        
        var temperatureSensors = hardware.Sensors
            .Where(sensor => sensor.SensorType == SensorType.Temperature && 
                             !sensor.Name.Contains("Average") && 
                             !sensor.Name.Contains("Max"))
            .ToList();
        
        ObservableCollectionHelpers.AdaptToSize(report.CpuInformation.Sensors, temperatureSensors.Count, () => new CpuSensor());
        
        for (var i = 0; i < temperatureSensors.Count; i++)
        {
            var temperatureSensor = temperatureSensors[i];
            
            report.CpuInformation.Sensors[i].Name = temperatureSensor.Name;
            report.CpuInformation.Sensors[i].Value = temperatureSensor.Value ?? 0;
            report.CpuInformation.Sensors[i].RoundedValue = (int) Math.Round(temperatureSensor.Value ?? 0);
        }
    }
}