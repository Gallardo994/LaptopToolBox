﻿using Ninject.Modules;

namespace GHelper.DeviceControls;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUsb>().To<AsusUsb>().InSingletonScope();
        Bind<IAcpiHandleProvider>().To<AsusAcpiHandleProvider>().InSingletonScope();
        Bind<IAcpi>().To<AsusAcpi>().InSingletonScope();
        Bind<IHid>().To<Hid>().InSingletonScope();
    }
}