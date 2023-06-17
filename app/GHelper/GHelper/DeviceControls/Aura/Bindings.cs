using Ninject.Modules;

namespace GHelper.DeviceControls.Aura;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAuraControl>().To<AuraControl>().InSingletonScope();
        Bind<IAuraModesProvider>().To<AuraModesProvider>().InSingletonScope();
    }
}