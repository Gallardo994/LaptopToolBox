using System.Collections.Generic;
using LaptopToolBox.DeviceControls.Display;
using LaptopToolBox.AlwaysAwake;
using LaptopToolBox.DeviceControls.Acpi;
using LaptopToolBox.DeviceControls.Display.NightLight;
using LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;
using LaptopToolBox.DeviceControls.Lighting;
using LaptopToolBox.DeviceControls.Microphone;
using LaptopToolBox.DeviceControls.PerformanceModes;
using LaptopToolBox.DeviceControls.TouchPad;
using LaptopToolBox.Notifications;
using Ninject;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus;

public class AsusKeyRegister : IVendorKeyRegister
{
    private readonly IVendorKeyboardHandler _keyboardHandler;
    private readonly IAcpi _acpi;
    private readonly IPerformanceModeControl _performanceModeControl;
    private readonly IDisplayNightLightController _displayNightLightController;
    private readonly IMicrophoneProvider _microphoneProvider;
    private readonly INotificationService _notificationService;
    private readonly IAlwaysAwakeController _alwaysAwakeController;
    private readonly IVendorKeyboardBacklightController _vendorKeyboardBacklightController;
    private readonly ITouchPadControl _touchPadControl;
    
    [Inject]
    public AsusKeyRegister(IVendorKeyboardHandler keyboardHandler, 
        IAcpi acpi, 
        IPerformanceModeControl performanceModeControl,
        IDisplayNightLightController displayNightLightController,
        IMicrophoneProvider microphoneProvider,
        INotificationService notificationService,
        IAlwaysAwakeController alwaysAwakeController,
        IVendorKeyboardBacklightController vendorKeyboardBacklightController,
        ITouchPadControl touchPadControl)
    {
        _keyboardHandler = keyboardHandler;
        _acpi = acpi;
        _performanceModeControl = performanceModeControl;
        _displayNightLightController = displayNightLightController;
        _microphoneProvider = microphoneProvider;
        _notificationService = notificationService;
        _alwaysAwakeController = alwaysAwakeController;
        _vendorKeyboardBacklightController = vendorKeyboardBacklightController;
        _touchPadControl = touchPadControl;
    }

    public void Register()
    {
        var keysList = new List<IVendorKeyBind>
        {
            new AsusBrightnessUpKeyBind(_acpi),
            new AsusBrightnessDownKeyBind(_acpi),
            new AsusOpenWindowKeyBind(),
            new AsusPerformanceModeKeyBind(_performanceModeControl),
            new AsusNightLightKeyBind(_displayNightLightController),
            new AsusMicrophoneToggleKeyBind(_microphoneProvider, _notificationService),
            new AsusAlwaysAwakeKeyBind(_alwaysAwakeController, _notificationService),
            new AsusKeyboardBacklightBrightnessUpKeyBind(_vendorKeyboardBacklightController),
            new AsusKeyboardBacklightBrightnessDownKeyBind(_vendorKeyboardBacklightController),
            new AsusToggleTouchpadKeyBind(_touchPadControl, _notificationService),
        };
        
        foreach (var keyBind in keysList)
        {
            _keyboardHandler.Bind(keyBind);
        }
    }
}