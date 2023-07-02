using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.Wmi;

namespace GHelper.ModelInfo
{
    public partial class ModelInfoProvider : ObservableObject, IModelInfoProvider
    {
        [ObservableProperty] private string _model;
        [ObservableProperty] private int _bios;

        public ModelInfoProvider(IWmiSessionFactory wmiSessionFactory)
        {
            using var session = wmiSessionFactory.CreateSession();
            var instances = session.QueryInstances(@"root\cimv2", "WQL", "SELECT SMBIOSBIOSVersion FROM Win32_BIOS");

            foreach (var instance in instances)
            {
                var biosVersionProp = instance.CimInstanceProperties["SMBIOSBIOSVersion"];
                
                if (biosVersionProp.Value is null)
                {
                    continue;
                }
                
                var stringVal = biosVersionProp.Value.ToString();
                
                if (string.IsNullOrEmpty(stringVal))
                {
                    continue;
                }
                
                var results = stringVal.Split(".");
                
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
}