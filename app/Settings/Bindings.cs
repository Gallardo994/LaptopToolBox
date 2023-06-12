using GHelper.Settings.Requests;
using Ninject.Modules;

namespace GHelper.Settings;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ISettingsOpenFormRequest>().To<SettingsOpenFormOpenFormRequest>().InSingletonScope();
        
        Bind<SettingsForm>().To<SettingsForm>().InSingletonScope();
        Bind<ISettingsFormController>().To<SettingsFormController>().InSingletonScope();
    }
}