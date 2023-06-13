using Ninject.Modules;

namespace GHelper;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IStartUpPage>().To<AppShell>().InSingletonScope();
        Bind<MainPage>().ToSelf().InSingletonScope();
        Bind<UpdatesPage>().ToSelf().InSingletonScope();
    }
}