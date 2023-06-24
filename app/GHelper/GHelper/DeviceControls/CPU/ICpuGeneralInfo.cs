using GHelper.Helpers;

namespace GHelper.DeviceControls.CPU;

public interface ICpuGeneralInfo : IObservableObject
{
    public string ProcessorId { get; }
    public string SocketDesignation { get; }
    public string ProcessorName { get; }
    public string Caption { get; }
    public ushort AddressWidth { get; }
    public ushort DataWidth { get; }
    public ushort Architecture { get; }
    public uint MaxClockSpeed { get; }
    public uint ExtClock { get; }
    public ulong L2CacheSize { get; }
    public ulong L3CacheSize { get; }
    public uint NumberOfCores { get; }
    public uint NumberOfLogicalProcessors { get; }
}