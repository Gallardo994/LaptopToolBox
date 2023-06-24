using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.CPU;
using GHelper.ExtraControls.Wallpaper;
using GHelper.Injection;
using GHelper.ModelInfo;
using Ninject;

namespace GHelper.ViewModels;

public class DeviceInformationViewModel : ObservableObject
{
    public IModelInfoProvider ModelInfo { get; } = Services.ResolutionRoot.Get<IModelInfoProvider>();
    public IWallpaperProvider Wallpaper { get; } = Services.ResolutionRoot.Get<IWallpaperProvider>();
    public ICpuFamilyProvider CpuFamily { get; } = Services.ResolutionRoot.Get<ICpuFamilyProvider>();

    public DeviceInformationViewModel()
    {
    }
}