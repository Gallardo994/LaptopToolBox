using System.Collections.Generic;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.Display;
using GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;
using GHelper.DeviceControls.Microphone;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Notifications;
using Ninject;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusKeyRegister : IVendorKeyRegister
{
    private readonly IVendorKeyboardHandler _keyboardHandler;
    private readonly IAcpi _acpi;
    private readonly IPerformanceModeControl _performanceModeControl;
    private readonly IDisplayNightLightController _displayNightLightController;
    private readonly IMicrophoneProvider _microphoneProvider;
    private readonly INotificationService _notificationService;
    
    [Inject]
    public AsusKeyRegister(IVendorKeyboardHandler keyboardHandler, 
        IAcpi acpi, 
        IPerformanceModeControl performanceModeControl,
        IDisplayNightLightController displayNightLightController,
        IMicrophoneProvider microphoneProvider,
        INotificationService notificationService)
    {
        _keyboardHandler = keyboardHandler;
        _acpi = acpi;
        _performanceModeControl = performanceModeControl;
        _displayNightLightController = displayNightLightController;
        _microphoneProvider = microphoneProvider;
        _notificationService = notificationService;
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
        };
        
        foreach (var keyBind in keysList)
        {
            _keyboardHandler.Bind(keyBind);
        }
    }
}