using GHelper.Helpers;

namespace GHelper.DeviceControls.GPUs;

public interface IGpuGeneralInfo : IObservableObject
{
    public string DeviceName { get; }
    public string DeviceId { get; }
    public string AdapterRam { get; }
    public string AdapterDacType { get; }
    public string Monochrome { get; }
    public string InstalledDisplayDrivers { get; }
    public string DriverVersion { get; }
    public string VideoProcessor { get; }
    public string VideoArchitecture { get; }
    public string VideoMemoryType { get; }
}