using System.Management;
using System.ServiceProcess;

namespace GHelper.Helpers;

public static class ServiceControllerExtensions
{
    public static void SetServiceAutoStartMode(this ServiceController serviceController, ServiceAutoStartMode mode)
    {
        using var m = new ManagementObject($"Win32_Service.Name=\"{serviceController.ServiceName}\"");
        m.InvokeMethod("ChangeStartMode", new object[] { mode.ToString() });
    }
}