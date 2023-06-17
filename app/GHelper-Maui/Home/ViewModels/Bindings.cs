using Ninject.Modules;

namespace GHelper.Home.ViewModels;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IMainPageViewModel>().To<MainPageViewModel>();
    }
}