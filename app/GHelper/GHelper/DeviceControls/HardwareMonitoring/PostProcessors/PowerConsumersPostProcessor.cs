using System;
using System.Collections.Generic;
using System.Linq;
using GHelper.DeviceControls.HardwareMonitoring.Data;
using GHelper.Helpers;
using LibreHardwareMonitor.Hardware;
using Serilog;

namespace GHelper.DeviceControls.HardwareMonitoring.PostProcessors;

public class PowerConsumersPostProcessor : IPostProcessor
{
    public void PostProcess(IHardwareReport report, IComputer computer)
    {
        var powerConsumers = new List<ISensor>();
        
        foreach (var hardware in computer.Hardware)
        {
            powerConsumers.AddRange(hardware.Sensors.Where(sensor => sensor.SensorType == SensorType.Power));
            powerConsumers.AddRange(from subHardware in hardware.SubHardware from sensor in subHardware.Sensors where sensor.SensorType == SensorType.Power select sensor);
        }
        
        ObservableCollectionHelpers.AdaptToSize(report.PowerConsumers, powerConsumers.Count, () => new PowerConsumer());
        
        for (var i = 0; i < powerConsumers.Count; i++)
        {
            report.PowerConsumers[i].Name = powerConsumers[i].Name;
            report.PowerConsumers[i].Value = powerConsumers[i].Value ?? 0;
            report.PowerConsumers[i].MaxValue = Math.Max(powerConsumers[i].Max ?? 0, 1);
            report.PowerConsumers[i].RoundedValue = (int) Math.Round(powerConsumers[i].Value ?? 0);
            report.PowerConsumers[i].RoundedMaxValue = (int) Math.Round(report.PowerConsumers[i].MaxValue);
            
            Log.Debug("Power Consumer {PowerConsumerName} has value {PowerConsumerValue}", report.PowerConsumers[i].Name, report.PowerConsumers[i].Value);
        }
    }
}