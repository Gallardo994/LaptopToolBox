﻿using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.HardwareMonitoring.Data.CPU;

public interface ICpuCoreInformation : IObservableObject
{
    public string Name { get; set; }
    public int CoreNumber { get; set; }
    public int TotalLoad { get; set; }
    
    public void Clear();
}