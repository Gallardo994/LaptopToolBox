using GHelper.Initializers.ConcreteInitializers;
using Ninject.Modules;

namespace GHelper.Initializers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<BatteryInitializer>().ToSelf().InSingletonScope();
        Bind<VendorKeyRegisterInitializer>().ToSelf().InSingletonScope();
        
        Bind<IInitializersProvider>().To<InitializersProvider>().InSingletonScope();
    }
}