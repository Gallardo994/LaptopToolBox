using System.Linq;
using LibreHardwareMonitor.Hardware;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Constructors;

public class GpuConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        var totalPower = hardware.Sensors.FirstOrDefault(sensor => sensor.SensorType == SensorType.Power);
        report.GpuInformation.TotalPower = (int) (totalPower?.Value ?? 0);
    }
}