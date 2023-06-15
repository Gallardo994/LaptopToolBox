using Ninject.Modules;

namespace GHelper.ViewModels;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesViewModel>().To<UpdatesViewModel>().InSingletonScope();
    }
}