using GHelper.DeviceControls.Microphone.Vendors.Asus;
using Ninject.Modules;

namespace GHelper.DeviceControls.Microphone;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IMicrophoneProvider>().To<AsusMicrophone>().InSingletonScope();
    }
}