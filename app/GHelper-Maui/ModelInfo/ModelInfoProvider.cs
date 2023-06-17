using System.Management;

namespace GHelper.ModelInfo;

public class ModelInfoProvider : IModelInfoProvider
{
    public string Model { get; }
    public int Bios { get; }

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
                if (int.TryParse(results[1], out var bios))
                {
                    Bios = bios;
                }
                return;
            }

            Model = results[0];
            Bios = 0;
        }
        
        Model = string.Empty;
        Bios = 0;
    }
}