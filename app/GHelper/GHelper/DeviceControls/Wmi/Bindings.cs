using Ninject.Modules;

namespace GHelper.DeviceControls.Wmi;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IWmiSessionFactory>().To<WmiSessionFactory>();
    }
}