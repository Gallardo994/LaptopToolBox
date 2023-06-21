using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GHelper.DeviceControls.PerformanceModes.Vendors.Asus;

public class AsusPerformanceModesProvider : IPerformanceModesProvider
{
    public ObservableCollection<IPerformanceMode> AvailableModes { get; init; }

    public AsusPerformanceModesProvider()
    {
        AvailableModes = new ObservableCollection<IPerformanceMode>
        {
            new IntegratedPerformanceMode
            {
                Id = Guid.Parse("3588c746-73fa-489b-bef6-039abf1599cd"),
                Title = "Silent",
                Description = "Save battery life, reduce heat and fan noise by reducing performance",
                Icon = "\uEC48",
                Type = PerformanceModeType.Silent,
            },
            new IntegratedPerformanceMode
            {
                Id = Guid.Parse("62465e2c-0093-4df5-b5f0-4790ce7a27af"),
                Title = "Balanced",
                Description = "Balance performance and battery life. This is the default mode",
                Icon = "\uEC49",
                Type = PerformanceModeType.Balanced,
            },
            new IntegratedPerformanceMode
            {
                Id = Guid.Parse("49e7cff7-0dc9-41c8-9ba7-6852321f5144"),
                Title = "Turbo",
                Description = "Maximize performance at the cost of battery life, temperatures and fan noise",
                Icon = "\uEC4A",
                Type = PerformanceModeType.Turbo,
            }
        };
    }
    
    public IPerformanceMode GetNextModeAfter(IPerformanceMode currentMode)
    {
        var currentModeIndex = AvailableModes.IndexOf(currentMode);
        var nextModeIndex = (currentModeIndex + 1) % AvailableModes.Count;
        return AvailableModes[nextModeIndex];
    }
    
    public IPerformanceMode FindById(Guid id)
    {
        return AvailableModes.FirstOrDefault(mode => mode.Id == id);
    }
}