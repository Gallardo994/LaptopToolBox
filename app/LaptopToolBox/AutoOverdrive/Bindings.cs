﻿using Ninject.Modules;

namespace LaptopToolBox.AutoOverdrive;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAutoOverdrive>().To<AutoOverdrive>().InSingletonScope();
    }
}