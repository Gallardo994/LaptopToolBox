using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Display.RefreshRate.Vendors.Asus;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IOverdriveController>().To<AsusOverdriveController>().InSingletonScope();
    }
}