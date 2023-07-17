using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Wmi;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IWmiSessionFactory>().To<WmiSessionFactory>();
    }
}