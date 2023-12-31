﻿using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Display.NightLight;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IDisplayNightLightController>().To<WindowsNightLightController>().InSingletonScope();
    }
}