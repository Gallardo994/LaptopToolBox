﻿using GHelper.AppWindows;
using GHelper.Helpers;
using GHelper.Injection;
using Ninject;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusOpenWindowKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 56;

    public void Execute()
    {
        Services.ResolutionRoot.Get<MainWindow>()
            .Show()
            .Restore()
            .Focus();
    }
}