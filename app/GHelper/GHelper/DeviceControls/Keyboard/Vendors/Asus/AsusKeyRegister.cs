using System.Collections.Generic;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;
using Ninject;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusKeyRegister : IVendorKeyRegister
{
    private readonly IVendorKeyboardHandler _keyboardHandler;
    private readonly IAcpi _acpi;
    
    [Inject]
    public AsusKeyRegister(IVendorKeyboardHandler keyboardHandler, 
        IAcpi acpi)
    {
        _keyboardHandler = keyboardHandler;
        _acpi = acpi;
    }
    
    public void Register()
    {
        var keysList = new List<IVendorKeyBind>
        {
            new AsusBrightnessUpKeyBind(_acpi),
            new AsusBrightnessDownKeyBind(_acpi),
            new AsusOpenWindowKeyBind(),
        };
        
        foreach (var keyBind in keysList)
        {
            _keyboardHandler.Bind(keyBind);
        }
    }
}