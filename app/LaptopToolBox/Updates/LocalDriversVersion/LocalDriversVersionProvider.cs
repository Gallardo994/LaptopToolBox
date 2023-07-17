using System.Collections.Generic;
using System.Linq;
using LaptopToolBox.DeviceControls.Wmi;
using Ninject;

namespace LaptopToolBox.Updates.LocalDriversVersion;

public class LocalDriversVersionProvider : ILocalDriversVersionProvider
{
    private readonly Dictionary<string, string> _data;
    private readonly IWmiSessionFactory _wmiSessionFactory;

    [Inject]
    public LocalDriversVersionProvider(IWmiSessionFactory wmiSessionFactory)
    {
        _wmiSessionFactory = wmiSessionFactory;
        _data = new Dictionary<string, string>();
    }

    public void Refresh()
    {
        Clear();

        using var session = _wmiSessionFactory.CreateSession();
        var instances = session.QueryInstances("root\\cimv2", "WQL", "Select * from Win32_PnPSignedDriver");

        foreach (var obj in instances)
        {
            var deviceID = obj.CimInstanceProperties["DeviceID"].Value?.ToString();
            var driverVersion = obj.CimInstanceProperties["DriverVersion"].Value?.ToString();

            if (deviceID != null && driverVersion != null)
            {
                Add(deviceID, driverVersion);
            }
        }
    }

    private void Add(string key, string value)
    {
        _data[key] = value;
    }

    private void Clear()
    {
        _data.Clear();
    }

    public string? GetLocalVersion(string deviceId)
    {
        return _data
            .Where(p => p.Key.Contains(deviceId))
            .Select(p => p.Value)
            .FirstOrDefault();
    }
}