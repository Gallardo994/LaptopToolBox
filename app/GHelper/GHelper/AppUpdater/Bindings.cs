﻿using Ninject.Modules;

namespace GHelper.AppUpdater;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdateProvider>().To<AppUpdateProvider>().InSingletonScope();
    }
}