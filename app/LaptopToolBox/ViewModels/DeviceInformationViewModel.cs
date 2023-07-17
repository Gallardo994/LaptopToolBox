using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.DeviceControls.CPU;
using LaptopToolBox.DeviceControls.GPUs;
using LaptopToolBox.ExtraControls.Wallpaper;
using LaptopToolBox.Injection;
using LaptopToolBox.ModelInfo;
using Ninject;

namespace LaptopToolBox.ViewModels;

public class DeviceInformationViewModel : ObservableObject
{
    public IModelInfoProvider ModelInfo { get; } = Services.ResolutionRoot.Get<IModelInfoProvider>();
    public IWallpaperProvider Wallpaper { get; } = Services.ResolutionRoot.Get<IWallpaperProvider>();
    public ICpuGeneralInfoProvider CpuGeneralInfo { get; } = Services.ResolutionRoot.Get<ICpuGeneralInfoProvider>();
    public IGpuGeneralInfoProvider GpuGeneralInfo { get; } = Services.ResolutionRoot.Get<IGpuGeneralInfoProvider>();

    public DeviceInformationViewModel()
    {
    }
}