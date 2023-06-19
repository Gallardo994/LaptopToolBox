﻿using System;

namespace GHelper.DeviceControls.Keyboard.Vendors;

public interface IVendorKeyBind
{
    public int Key { get; }
    public void Execute();
}