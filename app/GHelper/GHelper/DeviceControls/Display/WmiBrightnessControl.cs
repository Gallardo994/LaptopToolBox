using System;

namespace GHelper.DeviceControls.Display;
using System.Management;

public class WmiBrightnessControl : IBrightnessControl
{
    public int BrightnessStep { get; set; } = 10;

    public int Get()
    {
        using var mClass = new ManagementClass("WmiMonitorBrightness")
        {
            Scope = new ManagementScope(@"\\.\root\wmi")
        };
        using var instances = mClass.GetInstances();
        foreach (var o in instances)
        {
            var instance = (ManagementObject)o;
            return (byte)instance.GetPropertyValue("CurrentBrightness");
        }
        return 0;
    }

    public void Set(int brightness)
    {
        brightness = Math.Min(100, Math.Max(0, brightness));
        
        using var mClass = new ManagementClass("WmiMonitorBrightnessMethods")
        {
            Scope = new ManagementScope(@"\\.\root\wmi")
        };
        using var instances = mClass.GetInstances();
        var args = new object[] { 1, brightness };
        foreach (var o in instances)
        {
            var instance = (ManagementObject)o;
            instance.InvokeMethod("WmiSetBrightness", args);
        }
    }
    
    public void StepDown()
    {
        Set(Get() - BrightnessStep);
    }
    
    public void StepUp()
    {
        Set(Get() + BrightnessStep);
    }
}