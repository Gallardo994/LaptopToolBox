using Ninject.Modules;

namespace GHelper.Toasts;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IToastNativeWindow>().To<ToastNativeWindow>().InTransientScope();
        Bind<IToastIconResolver>().To<ToastIconResolver>().InSingletonScope();
        Bind<IToast>().To<Toast>().InTransientScope();
        Bind<IGlobalToastProvider>().To<GlobalToastProvider>().InSingletonScope();
    }
}