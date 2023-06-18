using Ninject.Modules;

namespace GHelper.DeviceControls.CPU;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ICpuControl>().To<AsusCpu>().InSingletonScope();
    }
}