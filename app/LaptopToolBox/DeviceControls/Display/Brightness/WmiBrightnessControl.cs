using System;
using LaptopToolBox.DeviceControls.Wmi;
using Microsoft.Management.Infrastructure;

namespace LaptopToolBox.DeviceControls.Display.Brightness;

public class WmiBrightnessControl : IBrightnessControl
{
    private readonly IWmiSessionFactory _wmiSessionFactory;
    
    public int BrightnessStep { get; set; } = 10;
    
    public WmiBrightnessControl(IWmiSessionFactory wmiSessionFactory)
    {
        _wmiSessionFactory = wmiSessionFactory;
    }

    public int Get()
    {
        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.EnumerateInstances(@"root\wmi", "WmiMonitorBrightness");
        foreach (var instance in instances)
        {
            return (byte)instance.CimInstanceProperties["CurrentBrightness"].Value;
        }
        return 0;
    }

    public void Set(int brightness)
    {
        brightness = Math.Min(100, Math.Max(0, brightness));
        
        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.EnumerateInstances(@"root\wmi", "WmiMonitorBrightnessMethods");
        var args = new CimMethodParametersCollection
        {
            CimMethodParameter.Create("Timeout", 1, CimType.UInt32, CimFlags.None), 
            CimMethodParameter.Create("Brightness", brightness, CimType.UInt32, CimFlags.None)
        };
        
        foreach (var instance in instances)
        {
            session.InvokeMethod(instance.CimSystemProperties.Namespace, instance, "WmiSetBrightness", args);
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