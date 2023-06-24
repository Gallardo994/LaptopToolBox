using Ninject.Modules;

namespace GHelper.AlwaysAwake;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAlwaysAwakeController>().To<KeyPressAlwaysAwake>();
    }
}