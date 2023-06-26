using GHelper.DeviceControls.TouchPad.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.TouchPad;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ITouchPadControl>().To<AsusTouchPadControl>();
    }
}