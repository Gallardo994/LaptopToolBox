using Ninject.Modules;

namespace LaptopToolBox.DeviceControls;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IHid>().To<Hid>().InSingletonScope();
    }
}