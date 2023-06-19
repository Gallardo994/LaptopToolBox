using System;
using GHelper.DeviceControls.Keyboard.Vendors;

namespace GHelper.DeviceControls.Keyboard;

public interface IVendorKeyboardHandler : IDisposable
{
    public void Bind(IVendorKeyBind keyBind);
    public void Unbind(IVendorKeyBind keyBind);
}