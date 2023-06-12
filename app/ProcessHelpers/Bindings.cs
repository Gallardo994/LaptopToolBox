using Ninject.Modules;

namespace GHelper.ProcessHelpers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IAdministratorHelper>().To<AdministratorHelper>();
    }
}