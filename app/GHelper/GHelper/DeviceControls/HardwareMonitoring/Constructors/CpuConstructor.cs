using System;
using System.Linq;
using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using OpenHardwareMonitor.Hardware;

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
        
        while (report.CpuInformation.CoresLoad.Count < cores.Count)
        {
            report.CpuInformation.CoresLoad.Add(new CpuCoreInformation());
        }
        
        while (report.CpuInformation.CoresLoad.Count > cores.Count)
        {
            report.CpuInformation.CoresLoad.RemoveAt(report.CpuInformation.CoresLoad.Count - 1);
        }
        
        for (var i = 0; i < cores.Count; i++)
        {
            var coreSensor = cores[i];
            
            report.CpuInformation.CoresLoad[i].Name = coreSensor.Name;
            report.CpuInformation.CoresLoad[i].CoreIndex = i;
            report.CpuInformation.CoresLoad[i].CoreNumber = i + 1;
            report.CpuInformation.CoresLoad[i].TotalLoad = (int) Math.Round(coreSensor.Value ?? 0);
        }
    }
}