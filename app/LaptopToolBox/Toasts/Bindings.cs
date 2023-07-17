using Ninject.Modules;

namespace LaptopToolBox.Toasts;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IToastController>().To<ToastController>().InSingletonScope();
    }
}