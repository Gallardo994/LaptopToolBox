using Ninject.Modules;

namespace GHelper.AsusAcpi;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAsusAcpiProvider>().To<AsusAcpiProvider>().InSingletonScope();
        Bind<IAsusAcpiErrorProvider>().To<AsusAcpiErrorProvider>().InSingletonScope();
    }
}