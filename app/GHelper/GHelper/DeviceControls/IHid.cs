using HidLibrary;

namespace GHelper.DeviceControls;

public interface IHid
{
    public HidDevice[] GetHidDevicesBlocking(int vendorId, int[] deviceIds, int minInput = 18, int minFeatures = 1);
    public HidDevice GetDevice(int vendorId, int[] deviceIds, byte reportId);
}