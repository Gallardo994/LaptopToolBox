using System.ServiceProcess;
using GHelper.Helpers;

namespace GHelper.VendorServices.Vendors.Asus;

public class AsusServicesDisableCommand : IAsusServiceCommand
{
    private readonly string[] _services;
    
    public AsusServicesDisableCommand(string[] services)
    {
        _services = services;
    }
    
    public void Execute()
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