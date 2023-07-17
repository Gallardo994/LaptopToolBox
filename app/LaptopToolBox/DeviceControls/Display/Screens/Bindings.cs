using Ninject.Modules;

namespace LaptopToolBox.DeviceControls.Display.Screens;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IScreenProvider>().To<ScreenProvider>().InSingletonScope();
    }
}