namespace GHelper.DeviceControls.CPU.Vendors.AMD;

public interface IAmdAddressesProvider
{
    public uint SMU_PCI_ADDR { get; set; }
    public uint SMU_OFFSET_ADDR { get; set; }
    public uint SMU_OFFSET_DATA { get; set; }

    public uint MP1_ADDR_MSG { get; set; }
    public uint MP1_ADDR_RSP { get; set; }
    public uint MP1_ADDR_ARG { get; set; }

    public uint PSMU_ADDR_MSG { get; set; }
    public uint PSMU_ADDR_RSP { get; set; }
    public uint PSMU_ADDR_ARG { get; set; }
}