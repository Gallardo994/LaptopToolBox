using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using Serilog;

namespace GHelper.DeviceControls.Display.NightLight;

public class WindowsNightLightController : IDisplayNightLightController
{
    private const string NightLightKey = @"Software\Microsoft\Windows\CurrentVersion\CloudStore\Store\DefaultAccount\Current\default$windows.data.bluelightreduction.bluelightreductionstate\windows.data.bluelightreduction.bluelightreductionstate\";

    public void SetNightLightState(bool state)
    {
        if (state)
        {
            EnableNightLight();
        }
        else
        {
            DisableNightLight();
        }
    }
    
    public bool IsNightLightEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(NightLightKey);
        
        var data = (byte[])key.GetValue("Data");
        return data.Length == 43 && data[18] == 0x15;
    }
    
    private void EnableNightLight()
    {
        ModifyNightLightStateData(false);
        Log.Debug("Night Light Enabled");
    }

    private void DisableNightLight()
    {
        ModifyNightLightStateData(true);
        Log.Debug("Night Light Disabled");
    }

    private void ModifyNightLightStateData(bool nightLightIsOn)
    {
        using var key = Registry.CurrentUser.OpenSubKey(NightLightKey, true);
        
        var data = (byte[])key.GetValue("Data");
        var list = data.ToList();

        if (nightLightIsOn)
        {
            IncrementValueAt(list, 10);
            list[18] = 0x13;
            list.RemoveAt(24);
            list.RemoveAt(23);
            list = list.Take(41).ToList();
        }
        else
        {
            IncrementValueAt(list, 10);
            list[18] = 0x15;
            list.Insert(23, 0x10);
            list.Insert(24, 0x00);
            while(list.Count < 43)
            {
                list.Add(0x00);
            }
        }

        key.SetValue("Data", list.ToArray());
    }

    private void IncrementValueAt(List<byte> data, int index)
    {
        while (true)
        {
            if (data[index] == 0xFF)
            {
                data[index] = 0x00;
                index += 1;
                continue;
            }
            
            data[index]++;

            break;
        }
    }
}