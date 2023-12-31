﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.CPU;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.GPU;
using LaptopToolBox.DeviceControls.HardwareMonitoring.Data.RAM;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring;

public partial class HardwareReport : ObservableObject, IHardwareReport
{
    [ObservableProperty] private IRamInformation _ramInformation;
    [ObservableProperty] private ICpuInformation _cpuInformation;
    [ObservableProperty] private IGpuInformation _gpuInformation;
    
    [ObservableProperty] private ObservableCollection<ITemperatureSensor> _sensors;
    [ObservableProperty] private ObservableCollection<IPowerConsumer> _powerConsumers;

    public HardwareReport()
    {
        RamInformation = new RamInformation();
        CpuInformation = new CpuInformation();
        GpuInformation = new GpuInformation();
        
        Sensors = new ObservableCollection<ITemperatureSensor>();
        PowerConsumers = new ObservableCollection<IPowerConsumer>();
    }

    public void Clear()
    {
        RamInformation.Clear();
        CpuInformation.Clear();
        GpuInformation.Clear();
        
        foreach (var sensor in Sensors)
        {
            sensor.Clear();
        }
        
        foreach (var powerConsumer in PowerConsumers)
        {
            powerConsumer.Clear();
        }
    }
}