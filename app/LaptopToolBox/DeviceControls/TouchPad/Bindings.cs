using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.TouchPad;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ITouchPadControl>().To<WindowsPnpTouchPadControl>();
    }
}