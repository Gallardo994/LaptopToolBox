using Ninject.Modules;

namespace GHelper.ViewModels;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<UpdatesViewModel>().ToSelf().InSingletonScope();
        Bind<AuraViewModel>().ToSelf().InSingletonScope();
        Bind<PerformanceModeViewModel>().ToSelf().InSingletonScope();
        Bind<UnderVoltViewModel>().ToSelf().InSingletonScope();
        Bind<TrayViewModel>().ToSelf().InSingletonScope();
        Bind<AutoStartViewModel>().ToSelf().InSingletonScope();
        Bind<BatteryLimitViewModel>().ToSelf().InSingletonScope();
        Bind<DeviceInformationViewModel>().ToSelf().InSingletonScope();
        Bind<StartMinimizedViewModel>().ToSelf().InSingletonScope();
        Bind<VendorServicesViewModel>().ToSelf().InSingletonScope();
        Bind<FansViewModel>().ToSelf().InSingletonScope();

        Bind<ModifyPerformanceProfileViewModel>().ToSelf().InTransientScope();
    }
}