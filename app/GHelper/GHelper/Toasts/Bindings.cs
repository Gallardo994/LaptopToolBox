using Ninject.Modules;

namespace GHelper.Toasts;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IToastController>().To<ToastController>().InSingletonScope();
    }
}