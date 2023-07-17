using System.Collections.Generic;

namespace LaptopToolBox.DeviceControls.Display.Screens;

public interface IScreenItem
{
    public uint GetRefreshRate();
    public void SetRefreshRate(uint refreshRate);
    public IReadOnlyList<uint> GetSupportedRefreshRates();
}