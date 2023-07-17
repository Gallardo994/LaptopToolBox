using Ninject.Modules;

namespace LaptopToolBox.Web;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IHttpClientFactory>().To<HttpClientFactory>().InSingletonScope();
    }
}