using Ninject.Modules;

namespace LaptopToolBox.AlwaysAwake;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAlwaysAwakeController>().To<KeyPressAlwaysAwake>();
    }
}