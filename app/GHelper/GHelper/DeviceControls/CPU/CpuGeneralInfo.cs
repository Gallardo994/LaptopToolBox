using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.DeviceControls.CPU;

public partial class CpuGeneralInfo : ObservableObject, ICpuGeneralInfo
{
    [ObservableProperty] private string _processorId;
    [ObservableProperty] private string _socketDesignation;
    [ObservableProperty] private string _processorName;
    [ObservableProperty] private string _caption;
    [ObservableProperty] private ushort _addressWidth;
    [ObservableProperty] private ushort _dataWidth;
    [ObservableProperty] private ushort _architecture;
    [ObservableProperty] private uint _maxClockSpeed;
    [ObservableProperty] private uint _extClock;
    [ObservableProperty] private ulong _l2CacheSize;
    [ObservableProperty] private ulong _l3CacheSize;
    [ObservableProperty] private uint _numberOfCores;
    [ObservableProperty] private uint _numberOfLogicalProcessors;

    public override string ToString()
    {
        return $"ProcessorId={ProcessorId}, SocketDesignation={SocketDesignation}, ProcessorName={ProcessorName}, Caption={Caption}, AddressWidth={AddressWidth}, DataWidth={DataWidth}, Architecture={Architecture}, MaxClockSpeed={MaxClockSpeed}, ExtClock={ExtClock}, L2CacheSize={L2CacheSize}, L3CacheSize={L3CacheSize}, NumberOfCores={NumberOfCores}, NumberOfLogicalProcessors={NumberOfLogicalProcessors}";
    }
}