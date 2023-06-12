using Ninject.Modules;

namespace GHelper.Settings;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<SettingsForm>().To<SettingsForm>().InSingletonScope();
        Bind<ISettingsFormController>().To<SettingsFormController>().InSingletonScope();
    }
}