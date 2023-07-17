using LaptopToolBox.AppUpdater.Downloaders.GitHub;
using Ninject.Modules;

namespace LaptopToolBox.AppUpdater.Downloaders;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAppUpdateDownloader>().To<GitHubAppUpdateDownloader>().InSingletonScope();
    }
}