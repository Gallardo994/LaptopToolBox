using Ninject.Modules;

namespace GHelper.Web;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IHttpClientFactory>().To<HttpClientFactory>().InSingletonScope();
    }
}