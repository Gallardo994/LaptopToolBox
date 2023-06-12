using Ninject.Modules;

namespace GHelper.Network;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<HttpClient>().ToSelf().InSingletonScope();
    }
}