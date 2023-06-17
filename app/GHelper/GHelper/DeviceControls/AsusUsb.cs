using Ninject;

namespace GHelper.DeviceControls;

public class AsusUsb : IUsb
{
    public int AsusId { get; init; }
    public int[] DeviceIds { get; init; }
    public byte AuraHidId { get; init; }
    public byte[] MessageSet { get; init; }
    public byte[] MessageApply { get; init; }

    [Inject]
    public AsusUsb()
    {
        AsusId = 0x0b05;
        DeviceIds = new []{ 0x1a30, 0x1854, 0x1869, 0x1866, 0x19b6, 0x1822, 0x1837, 0x1854, 0x184a, 0x183d, 0x8502, 0x1807, 0x17e0, 0x18c6 };
        AuraHidId = 0x5d;
        
        MessageSet = new byte[]{ AuraHidId, 0xb5, 0, 0, 0 };
        MessageApply = new byte[]{ AuraHidId, 0xb4 };
    }
}