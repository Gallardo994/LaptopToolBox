using System.Collections.Generic;
using GHelper.Commands;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;
using GHelper.DeviceControls.PerformanceModes;
using Ninject;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusKeyRegister : IVendorKeyRegister
{
    private readonly IVendorKeyboardHandler _keyboardHandler;
    private readonly IAcpi _acpi;
    private readonly IPerformanceModeControl _performanceModeControl;
    
    [Inject]
    public AsusKeyRegister(IVendorKeyboardHandler keyboardHandler, 
        IAcpi acpi, 
        IPerformanceModeControl performanceModeControl)
    {
        _keyboardHandler = keyboardHandler;
        _acpi = acpi;
        _performanceModeControl = performanceModeControl;
    }

    public void Register()
    {
        var keysList = new List<IVendorKeyBind>
        {
            new AsusBrightnessUpKeyBind(_acpi),
            new AsusBrightnessDownKeyBind(_acpi),
            new AsusOpenWindowKeyBind(),
            new AsusPerformanceModeKeyBind(_performanceModeControl),
        };
        
        foreach (var keyBind in keysList)
        {
            _keyboardHandler.Bind(keyBind);
        }
    }
}