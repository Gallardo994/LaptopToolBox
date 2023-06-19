namespace GHelper.DeviceControls.Usb.Vendors.Asus;

public class AsusUsb : IUsb
{
    public int VendorId { get; } = 0x0b05;
    public int[] DeviceIds { get; } = { 0x1a30, 0x1854, 0x1869, 0x1866, 0x19b6, 0x1822, 0x1837, 0x1854, 0x184a, 0x183d, 0x8502, 0x1807, 0x17e0, 0x18c6 };
    public byte LightingHidId { get; } = 0x5d;
    public byte InputHidId { get; } = 0x5a;
}