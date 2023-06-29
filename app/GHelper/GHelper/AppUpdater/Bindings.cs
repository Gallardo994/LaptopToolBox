using GHelper.AppUpdater.GitHub;
using Ninject.Modules;

namespace GHelper.AppUpdater;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdater>().To<GitHubAppUpdater>().InSingletonScope();
    }
}