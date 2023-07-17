using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Display.Brightness;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IBrightnessControl>().To<WmiBrightnessControl>().InSingletonScope();
    }
}