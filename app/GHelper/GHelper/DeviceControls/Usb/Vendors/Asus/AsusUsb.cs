using Ninject;

namespace GHelper.DeviceControls.Usb.Vendors.Asus;

public class AsusUsb : IUsb
{
    public int VendorId { get; init; }
    public int[] DeviceIds { get; init; }
    public byte LightingHidId { get; init; }
    public byte InputHidId { get; init; }

    [Inject]
    public AsusUsb()
    {
        VendorId = 0x0b05;
        DeviceIds = new []{ 0x1a30, 0x1854, 0x1869, 0x1866, 0x19b6, 0x1822, 0x1837, 0x1854, 0x184a, 0x183d, 0x8502, 0x1807, 0x17e0, 0x18c6 };
        
        LightingHidId = 0x5d;
        InputHidId = 0x5a;
    }
}