using System;
using System.Linq;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.DeviceControls.HardwareMonitoring.Data.CPU;
using GHelper.Helpers;
using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring.Constructors;

public class CpuConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        var total = hardware.Sensors.FirstOrDefault(sensor => sensor.SensorType == SensorType.Load && sensor.Name.Contains("Total"));
        report.CpuInformation.TotalLoad = (int) (total?.Value ?? 0);
        
        var totalPower = hardware.Sensors.FirstOrDefault(sensor => sensor.SensorType == SensorType.Power && sensor.Name.Contains("Package"));
        report.CpuInformation.TotalPower = (int) (totalPower?.Value ?? 0);
        
        var coresLoad = hardware.Sensors
            .Where(sensor => sensor.SensorType == SensorType.Load && sensor.Name.Contains("Core"))
            .ToList();

        ObservableCollectionHelpers.AdaptToSize(report.CpuInformation.CoresLoad, coresLoad.Count, () => new CpuCoreInformation());

        for (var i = 0; i < coresLoad.Count; i++)
        {
            var coreSensor = coresLoad[i];
            
            report.CpuInformation.CoresLoad[i].Name = coreSensor.Name;
            report.CpuInformation.CoresLoad[i].CoreNumber = i + 1;
            report.CpuInformation.CoresLoad[i].TotalLoad = (int) Math.Round(coreSensor.Value ?? 0);
        }
    }
}