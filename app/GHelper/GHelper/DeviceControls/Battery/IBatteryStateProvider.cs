using System;

namespace GHelper.DeviceControls.Battery;

public interface IBatteryStateProvider
{
    public event Action<PowerState> PowerStateChanged;
    public PowerState CurrentPowerState { get; }
}