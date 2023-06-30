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
        var totalPower = hardware.Sensors.FirstOrDefault(sensor => sensor.SensorType == SensorType.Power);
        report.GpuInformation.TotalPower = (int) (totalPower?.Value ?? 0);
    }
}