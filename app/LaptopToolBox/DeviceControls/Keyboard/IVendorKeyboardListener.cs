using System;

namespace LaptopToolBox.DeviceControls.Keyboard;

public interface IVendorKeyboardListener : IDisposable
{
    public Action<int> KeyHandler { get; set; }
}