using GHelper.DeviceControls.Fans.Vendors;
using Ninject.Modules;

namespace GHelper.DeviceControls.Fans;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IFanController>().To<AsusFanController>().InSingletonScope();
    }
}