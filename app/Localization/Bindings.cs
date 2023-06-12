using Ninject.Modules;

namespace GHelper.Localization;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ILanguageSetter>().To<LanguageSetter>().InSingletonScope();
    }
}