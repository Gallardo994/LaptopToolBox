using GHelper.AppUpdater.Downloaders.GitHub;
using Ninject.Modules;

namespace GHelper.AppUpdater.Downloaders;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdateDownloader>().To<GitHubAppUpdateDownloader>().InSingletonScope();
    }
}