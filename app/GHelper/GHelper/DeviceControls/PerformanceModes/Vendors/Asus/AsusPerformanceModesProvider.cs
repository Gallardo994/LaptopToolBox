﻿using System.Collections.ObjectModel;

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
                Title = "Silent",
                Description = "Save battery life, reduce heat and fan noise by reducing performance",
                Icon = "\uEC48",
                Type = PerformanceModeType.Silent,
            },
            new IntegratedPerformanceMode
            {
                Title = "Balanced",
                Description = "Balance performance and battery life. This is the default mode",
                Icon = "\uEC49",
                Type = PerformanceModeType.Balanced,
            },
            new IntegratedPerformanceMode
            {
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
}