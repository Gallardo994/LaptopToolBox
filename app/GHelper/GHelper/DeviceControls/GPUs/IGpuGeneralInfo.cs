using GHelper.Helpers;

namespace GHelper.DeviceControls.GPUs;

public interface IGpuGeneralInfo : IObservableObject
{
    public string DeviceName { get; }
    public string DeviceId { get; }
    public uint AdapterRam { get; }
    public string AdapterDacType { get; }
    public bool Monochrome { get; }
    public string InstalledDisplayDrivers { get; }
    public string DriverVersion { get; }
    public string VideoProcessor { get; }
    public ushort VideoArchitecture { get; }
    public ushort VideoMemoryType { get; }
}