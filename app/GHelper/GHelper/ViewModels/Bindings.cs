using Ninject.Modules;

namespace GHelper.ViewModels;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<UpdatesViewModel>().ToSelf().InSingletonScope();
        Bind<AuraViewModel>().ToSelf().InSingletonScope();
        Bind<PerformanceModeViewModel>().ToSelf().InSingletonScope();
        Bind<UnderVoltViewModel>().ToSelf().InSingletonScope();
        Bind<TrayViewModel>().ToSelf().InSingletonScope();
    }
}