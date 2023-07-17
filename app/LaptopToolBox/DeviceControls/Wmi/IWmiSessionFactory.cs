using Microsoft.Management.Infrastructure;

namespace LaptopToolBox.DeviceControls.Wmi;

public interface IWmiSessionFactory
{
    public CimSession CreateSession();
}