using System.Linq;
using HidLibrary;

namespace GHelper.DeviceControls;

public class Hid : IHid
{
    public HidDevice[] GetHidDevicesBlocking(int vendorId, int[] deviceIds, int minInput = 18, int minFeatures = 1)
    {
        return HidDevices
            .Enumerate(vendorId, deviceIds)
            .Select(device => device)
            .Where(device => device != null
                             && device.IsConnected
                             && device.Capabilities.FeatureReportByteLength >= minFeatures
                             && device.Capabilities.InputReportByteLength >= minInput)
            .ToArray();
    }
    
    public HidDevice GetDevice(int vendorId, int[] deviceIds, byte reportId)
    {
        var hidDeviceList = HidDevices.Enumerate(vendorId, deviceIds).ToArray();
        var input = default(HidDevice);

        foreach (var device in hidDeviceList)
        {
            if (device.ReadFeatureData(out _, reportId))
            {
                input = device;
            }
        }

        return input;
    }
}