using LaptopToolBox.AppWindows;
using Ninject.Modules;

namespace LaptopToolBox;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IPageProvider>().To<PageProvider>().InSingletonScope();
        Bind<ToastWindow>().ToSelf().InSingletonScope();
        Bind<MainWindow>().ToSelf().InSingletonScope();
    }
}