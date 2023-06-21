using Ninject.Modules;

namespace GHelper.ExtraControls;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IWallpaperProvider>().To<DesktopWallpaperProvider>().InSingletonScope();
    }
}