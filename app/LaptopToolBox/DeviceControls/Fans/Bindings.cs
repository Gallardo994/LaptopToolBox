using LaptopToolBox.DeviceControls.Fans.Vendors;
using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Fans;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IFanController>().To<AsusFanController>().InSingletonScope();
    }
}