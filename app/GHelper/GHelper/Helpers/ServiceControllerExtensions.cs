using Microsoft.Management.Infrastructure;
using System.ServiceProcess;

namespace GHelper.Helpers;

public static class ServiceControllerExtensions
{
    public static void SetServiceAutoStartMode(this ServiceController serviceController, ServiceAutoStartMode mode)
    {
        using var session = CimSession.Create("localhost");
        
        var instances = session.QueryInstances("root\\cimv2", "WQL", 
            $"SELECT * FROM Win32_Service WHERE Name = '{serviceController.ServiceName}'");

        foreach (var instance in instances)
        {
            var args = new CimMethodParametersCollection
            {
                CimMethodParameter.Create("StartMode", mode.ToString(), CimFlags.In)
            };

            session.InvokeMethod(instance.CimSystemProperties.Namespace, instance, "ChangeStartMode", args);
        }
    }
}