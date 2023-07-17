using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using Vanara.PInvoke;

namespace LaptopToolBox.DeviceControls.Display.Screens;

public partial class ScreenProvider : ObservableObject, IScreenProvider
{
    [ObservableProperty] private ObservableCollection<IScreenItem> _items;

    public ScreenProvider()
    {
        Refresh();
    }

    public void Refresh()
    {
        var items = new ObservableCollection<IScreenItem>();

        foreach (var device in GetAllDevices())
        {
            var screenItem = new GdiScreenItem
            {
                DeviceId = device,
            };

            items.Add(screenItem);
        }

        Items = items;
    }
    
    private List<GdiDeviceId> GetAllDevices()
    {
        var deviceIds = new List<GdiDeviceId>();

        var deviceName = new Gdi32.DISPLAY_DEVICE();
        deviceName.cb = (uint)Marshal.SizeOf(typeof(Gdi32.DISPLAY_DEVICE));

        for (uint id = 0; User32.EnumDisplayDevices(null, id, ref deviceName, 0); id++)
        {
            if (deviceName.StateFlags.HasFlag(Gdi32.DISPLAY_DEVICE_FLAGS.DISPLAY_DEVICE_ATTACHED_TO_DESKTOP))
            {
                var device = new GdiDeviceId
                {
                    LpszDeviceName = deviceName.DeviceName,
                };
                deviceIds.Add(device);
            }

            deviceName.cb = (uint)Marshal.SizeOf(typeof(Gdi32.DISPLAY_DEVICE));
        }

        return deviceIds;
    }
}