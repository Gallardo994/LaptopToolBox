using Microsoft.Management.Infrastructure;

namespace GHelper.DeviceControls.Wmi;

public interface IWmiSessionFactory
{
    public CimSession CreateSession();
}