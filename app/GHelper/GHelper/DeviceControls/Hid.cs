using System.Collections.Generic;
using System.Linq;
using HidLibrary;
using Ninject;

namespace GHelper.DeviceControls;

public class Hid : IHid
{
    private readonly IUsb _usb;

    [Inject]
    public Hid(IUsb usb)
    {
        _usb = usb;
    }

    public HidDevice[] GetHidDevicesBlocking(int[] deviceIds, int minInput = 18, int minFeatures = 1)
    {
        return HidDevices
            .Enumerate(_usb.AsusId, deviceIds)
            .Select(device => device)
            .Where(device => device != null
                             && device.IsConnected
                             && device.Capabilities.FeatureReportByteLength >= minFeatures
                             && device.Capabilities.InputReportByteLength >= minInput)
            .ToArray();
    }
}