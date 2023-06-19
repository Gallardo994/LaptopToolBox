using Ninject.Modules;

namespace GHelper.DeviceControls;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IHid>().To<Hid>().InSingletonScope();
    }
}