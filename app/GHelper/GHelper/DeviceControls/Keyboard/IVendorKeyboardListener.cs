using System;

namespace GHelper.DeviceControls.Keyboard;

public interface IVendorKeyboardListener : IDisposable
{
    public Action<int> KeyHandler { get; set; }
}