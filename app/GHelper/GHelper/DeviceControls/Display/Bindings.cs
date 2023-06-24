using Ninject.Modules;

namespace GHelper.DeviceControls.Display;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBrightnessControl>().To<WmiBrightnessControl>().InSingletonScope();
        Bind<IDisplayNightLightController>().To<WindowsNightLightController>().InSingletonScope();
    }
}