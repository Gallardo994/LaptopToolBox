using Microsoft.Management.Infrastructure;

namespace LaptopToolBox.DeviceControls.Wmi;

public class WmiSessionFactory : IWmiSessionFactory
{
    public CimSession CreateSession()
    {
        return CimSession.Create(null);
    }
}