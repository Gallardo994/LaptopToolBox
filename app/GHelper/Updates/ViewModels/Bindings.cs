using Ninject.Modules;

namespace GHelper.Updates.ViewModels;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesViewModel>().To<UpdatesViewModel>().InSingletonScope();
    }
}