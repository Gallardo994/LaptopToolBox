using Ninject.Modules;

namespace GHelper.ExtraControls.Wallpaper;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IWallpaperProvider>().To<DesktopWallpaperProvider>().InSingletonScope();
    }
}