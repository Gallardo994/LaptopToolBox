using Ninject.Modules;

namespace GHelper;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IStartUpPage>().To<AppFlyout>().InSingletonScope();
        Bind<MainPage>().ToSelf().InSingletonScope();
        Bind<UpdatesPage>().ToSelf().InSingletonScope();
        Bind<SettingsPage>().ToSelf().InSingletonScope();
    }
}