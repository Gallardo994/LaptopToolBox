using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace LaptopToolBox.DeviceControls.Display.Screens;

public class GdiScreenItem : IScreenItem
{
    public GdiDeviceId DeviceId { get; init; }

    public uint GetRefreshRate()
    {
        var devMode = new DEVMODE();
        devMode.dmSize = (ushort) Marshal.SizeOf(typeof(DEVMODE));
        
        if (User32.EnumDisplaySettings(DeviceId.LpszDeviceName, User32.ENUM_CURRENT_SETTINGS, ref devMode))
        {
            return devMode.dmDisplayFrequency;
        }

        return 0;
    }
    
    public void SetRefreshRate(uint refreshRate)
    {
        var devMode = new DEVMODE();
        devMode.dmSize = (ushort) Marshal.SizeOf(typeof(DEVMODE));
        
        if (User32.EnumDisplaySettings(DeviceId.LpszDeviceName, User32.ENUM_CURRENT_SETTINGS, ref devMode))
        {
            devMode.dmDisplayFrequency = refreshRate;
            User32.ChangeDisplaySettings(in devMode, User32.ChangeDisplaySettingsFlags.CDS_UPDATEREGISTRY);
        }
        else
        {
            throw new Exception($"Failed to retrieve current display settings for screen {DeviceId}");
        }
    }
    
    public IReadOnlyList<uint> GetSupportedRefreshRates()
    {
        var devMode = new DEVMODE();
        devMode.dmSize = (ushort) Marshal.SizeOf(typeof(DEVMODE));
        
        var refreshRates = new List<uint>();
        
        for (uint i = 0; User32.EnumDisplaySettings(DeviceId.LpszDeviceName, i, ref devMode); i++)
        {
            refreshRates.Add(devMode.dmDisplayFrequency);
        }

        return refreshRates.Distinct().OrderBy(x => x).ToList();
    }

    public override string ToString()
    {
        return $"{nameof(DeviceId)}={DeviceId}";
    }
}