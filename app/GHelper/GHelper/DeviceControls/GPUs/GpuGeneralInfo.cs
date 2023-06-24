using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.GPUs;

public partial class GpuGeneralInfo : ObservableObject, IGpuGeneralInfo
{
    [ObservableProperty] private string _deviceName;
    [ObservableProperty] private string _deviceId;
    [ObservableProperty] private uint _adapterRam;
    [ObservableProperty] private string _adapterDacType;
    [ObservableProperty] private bool _monochrome;
    [ObservableProperty] private string _installedDisplayDrivers;
    [ObservableProperty] private string _driverVersion;
    [ObservableProperty] private string _videoProcessor;
    [ObservableProperty] private ushort _videoArchitecture;
    [ObservableProperty] private ushort _videoMemoryType;

    public override string ToString()
    { 
        return $"DeviceName={DeviceName}, DeviceId={DeviceId}, AdapterRam={AdapterRam}, AdapterDacType={AdapterDacType}, Monochrome={Monochrome}, InstalledDisplayDrivers={InstalledDisplayDrivers}, DriverVersion={DriverVersion}, VideoProcessor={VideoProcessor}, VideoArchitecture={VideoArchitecture}, VideoMemoryType={VideoMemoryType}";
    }
}