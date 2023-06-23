using System.Collections.Generic;
using System.ServiceProcess;
using GHelper.Helpers;

namespace GHelper.VendorServices.Vendors.Asus;

public class AsusServicesControl : IVendorServicesControl
{
    private readonly HashSet<string> _services = new()
    {
        "AsusAppService",
        "ASUSLinkNear",
        "ASUSLinkRemote",
        "ASUSSoftwareManager",
        "ASUSSwitch",
        "ASUSSystemAnalysis",
        "ASUSSystemDiagnosis",
        "ArmouryCrateControlInterface",
        "AsusCertService",
        "ASUSOptimization",
    };
    
    public int CountRunning()
    {
        var count = 0;
        
        foreach (var service in _services)
        {
            using var serviceController = new ServiceController(service);
            if (serviceController.Status == ServiceControllerStatus.Running)
            {
                count++;
            }
        }

        return count;
    }

    public void Enable()
    {
        foreach (var service in _services)
        {
            using var serviceController = new ServiceController(service);
            if (serviceController.Status != ServiceControllerStatus.Stopped)
            {
                continue;
            }
            
            serviceController.Start();
            serviceController.SetServiceAutoStartMode(ServiceAutoStartMode.Automatic);
        }
    }

    public void Disable()
    {
        foreach (var service in _services)
        {
            using var serviceController = new ServiceController(service);
            if (serviceController.Status != ServiceControllerStatus.Running)
            {
                continue;
            }
            
            serviceController.Stop();
            serviceController.SetServiceAutoStartMode(ServiceAutoStartMode.Disabled);
        }
    }
}