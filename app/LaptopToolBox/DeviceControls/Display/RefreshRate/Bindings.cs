﻿using LaptopToolBox.DeviceControls.Display.RefreshRate.Vendors.Asus;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Display.RefreshRate;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IRefreshRateController>().To<AsusRefreshRateController>().InSingletonScope();
    }
}