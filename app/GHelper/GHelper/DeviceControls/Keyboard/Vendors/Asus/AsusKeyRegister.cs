using System.Collections.Generic;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;
using Ninject;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusKeyRegister : IVendorKeyRegister
{
    [Inject]
    public AsusKeyRegister(IVendorKeyboardHandler keyboardHandler, 
        IAcpi acpi)
    {
        var keysList = new List<IVendorKeyBind>
        {
            new AsusBrightnessUpKeyBind(acpi),
            new AsusBrightnessDownKeyBind(acpi),
        };
        
        foreach (var keyBind in keysList)
        {
            keyboardHandler.Bind(keyBind);
        }
    }
}