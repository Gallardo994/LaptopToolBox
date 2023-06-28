using Microsoft.Management.Infrastructure;

namespace GHelper.DeviceControls.Wmi;

public class WmiSessionFactory : IWmiSessionFactory
{
    public CimSession CreateSession()
    {
        return CimSession.Create(null);
    }
}