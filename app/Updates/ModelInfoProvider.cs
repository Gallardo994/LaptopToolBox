using System.Management;

namespace GHelper.Updates;

public class ModelInfoProvider : IModelInfoProvider
{
    public string Model { get; }
    public string Bios { get; }
    
    public int GetNumericBiosVersion()
    {
        return int.TryParse(Bios, out var result) ? result : 0;
    }
    
    public ModelInfoProvider()
    {
        using var objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
        using var objCollection = objSearcher.Get();
        
        foreach (ManagementObject obj in objCollection)
        {
            if (obj["SMBIOSBIOSVersion"] is null)
            {
                continue;
            }
                
            var results = obj["SMBIOSBIOSVersion"].ToString().Split(".");
                    
            if (results.Length > 1)
            {
                Model = results[0];
                Bios = results[1];

                return;
            }

            Model = results[0];
            Bios = null;
        }
        
        Model = null;
        Bios = null;
    }
}