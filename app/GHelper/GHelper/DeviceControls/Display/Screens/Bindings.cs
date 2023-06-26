using Ninject.Modules;

namespace GHelper.DeviceControls.Display.Screens;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IScreenProvider>().To<ScreenProvider>().InSingletonScope();
    }
}