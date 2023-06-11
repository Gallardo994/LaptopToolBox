using GHelper.Inputs;
using Ninject.Modules;

namespace GHelper.Modules;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IInputDispatcher>().To<InputDispatcher>().InSingletonScope();
    }
}