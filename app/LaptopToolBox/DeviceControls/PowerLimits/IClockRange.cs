﻿namespace LaptopToolBox.DeviceControls.PowerLimits;

public interface IClockRange
{
    public int Default { get; }
    public int Min { get; }
    public int Max { get; }
}