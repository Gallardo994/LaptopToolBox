﻿using System;
using System.Linq;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.RAM;
using LibreHardwareMonitor.Hardware;
using Serilog;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Constructors;

public class MemoryConstructor : IConstructor
{
    public void FillReport(IHardwareReport report, IHardware hardware)
    {
        report.RamInformation = new RamInformation();
        
        var memoryUsedSensor = hardware.Sensors.FirstOrDefault(sensor => sensor.Name == "Memory Used");
        if (memoryUsedSensor != null)
        {
            report.RamInformation.Used = memoryUsedSensor.Value ?? 0;
        }
        
        var memoryAvailableSensor = hardware.Sensors.FirstOrDefault(sensor => sensor.Name == "Memory Available");
        if (memoryAvailableSensor != null)
        {
            report.RamInformation.Available = memoryAvailableSensor.Value ?? 0;
        }
        
        report.RamInformation.Total = report.RamInformation.Used + report.RamInformation.Available;
        
        if (report.RamInformation.Total == 0)
        {
            report.RamInformation.PercentageUsed = 0;
        }
        else
        {
            report.RamInformation.PercentageUsed = (int) Math.Round(report.RamInformation.Used / report.RamInformation.Total * 100);
        }
    }
}