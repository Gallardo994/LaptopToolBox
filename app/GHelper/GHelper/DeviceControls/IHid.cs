using HidLibrary;

namespace GHelper.DeviceControls;

public interface IHid
{
    public HidDevice[] GetHidDevicesBlocking(int[] deviceIds, int minInput = 18, int minFeatures = 1);
}