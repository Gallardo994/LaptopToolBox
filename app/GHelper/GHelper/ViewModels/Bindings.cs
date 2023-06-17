using GHelper.DeviceControls.Aura;
using Ninject.Modules;

namespace GHelper.ViewModels;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IUpdatesViewModel>().To<UpdatesViewModel>().InSingletonScope();
        Bind<IAuraViewModel>().To<AuraViewModel>().InSingletonScope();
    }
}