using System;
using LaptopToolBox.DeviceControls.Keyboard.Vendors;

namespace LaptopToolBox.DeviceControls.Keyboard;

public interface IVendorKeyboardHandler : IDisposable
{
    public void Bind(IVendorKeyBind keyBind);
    public void Unbind(IVendorKeyBind keyBind);
}