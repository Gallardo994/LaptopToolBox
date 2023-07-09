using System;
using System.ServiceProcess;
using GHelper.Helpers;

namespace GHelper.VendorServices.Vendors.Asus;

public class AsusServicesEnableCommand : IAsusServiceCommand
{
    private readonly string[] _services;
    
    public AsusServicesEnableCommand(string[] services)
    {
        _services = services;
    }
    
    public void Execute()
    {
        foreach (var service in _services)
        {
            try
            {
                using var serviceController = new ServiceController(service);
            
                serviceController.SetServiceAutoStartMode(ServiceAutoStartMode.Automatic);
            
                if (serviceController.Status != ServiceControllerStatus.Stopped)
                {
                    continue;
                }
            
                serviceController.Start();
            }
            catch (InvalidOperationException)
            {
                // Service doesn't exist
            }
        }
    }
}