using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.GPUs;

public partial class GpuGeneralInfo : ObservableObject, IGpuGeneralInfo
{
    [ObservableProperty] private string _deviceName;
    [ObservableProperty] private string _deviceId;
    [ObservableProperty] private string _adapterRam;
    [ObservableProperty] private string _adapterDacType;
    [ObservableProperty] private string _monochrome;
    [ObservableProperty] private string _installedDisplayDrivers;
    [ObservableProperty] private string _driverVersion;
    [ObservableProperty] private string _videoProcessor;
    [ObservableProperty] private string _videoArchitecture;
    [ObservableProperty] private string _videoMemoryType;
}