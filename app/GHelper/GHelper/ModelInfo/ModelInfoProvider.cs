using System.Management;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.ModelInfo;

public partial class ModelInfoProvider : ObservableObject, IModelInfoProvider
{
    [ObservableProperty] private string _model;
    [ObservableProperty] private int _bios;

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