using System;
using System.Collections.ObjectModel;
using System.Linq;
using GHelper.Configs;
using GHelper.DeviceControls.Fans;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.PerformanceModes.Vendors.Asus;

public class AsusPerformanceModesProvider : IPerformanceModesProvider
{
    private readonly IConfig _config;
    private readonly IFanController _fanController;
    
    public ObservableCollection<IPerformanceMode> AvailableModes { get; init; }

    [Inject]
    public AsusPerformanceModesProvider(IConfig config, IFanController fanController)
    {
        _config = config;
        _fanController = fanController;
        
        AvailableModes = new ObservableCollection<IPerformanceMode>
        {
            new IntegratedPerformanceMode
            {
                Id = Guid.Parse("3588c746-73fa-489b-bef6-039abf1599cd"),
                Title = "Silent",
                Description = "Save battery life, reduce heat and fan noise by reducing performance",
                Icon = "\uEC48",
                Type = PerformanceModeType.Silent,
                IsAvailableOnStartup = true,
                IsAvailableInHotkeys = true,
            },
            new IntegratedPerformanceMode
            {
                Id = Guid.Parse("62465e2c-0093-4df5-b5f0-4790ce7a27af"),
                Title = "Balanced",
                Description = "Balance performance and battery life. This is the default mode",
                Icon = "\uEC49",
                Type = PerformanceModeType.Balanced,
                IsAvailableOnStartup = true,
                IsAvailableInHotkeys = true,
            },
            new IntegratedPerformanceMode
            {
                Id = Guid.Parse("49e7cff7-0dc9-41c8-9ba7-6852321f5144"),
                Title = "Turbo",
                Description = "Maximize performance at the cost of battery life, temperatures and fan noise",
                Icon = "\uEC4A",
                Type = PerformanceModeType.Turbo,
                IsAvailableOnStartup = true,
                IsAvailableInHotkeys = true,
            }
        };
        
        foreach (var customPerformanceMode in _config.CustomPerformanceModes)
        {
            AvailableModes.Add(customPerformanceMode);
        }
    }
    
    public IPerformanceMode GetNextModeAfter(IPerformanceMode currentMode)
    {
        var availableModes = AvailableModes.Where(mode => mode.IsAvailableInHotkeys).ToList();
        
        var currentModeIndex = availableModes.IndexOf(currentMode);
        var nextModeIndex = (currentModeIndex + 1) % availableModes.Count;
        return availableModes[nextModeIndex];
    }
    
    public IPerformanceMode FindById(Guid id)
    {
        return AvailableModes.FirstOrDefault(mode => mode.Id == id);
    }
    
    public IPerformanceMode CreateCustomPerformanceMode(string title, string description = "")
    {
        var newMode = new CustomPerformanceMode
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            IsAvailableOnStartup = false,
            IsAvailableInHotkeys = true,
            CpuFanCurve = new FanCurve(_fanController.FanCurvePointCount),
            GpuFanCurve = new FanCurve(_fanController.FanCurvePointCount),
        };
        
        AvailableModes.Add(newMode);
        _config.CustomPerformanceModes.Add(newMode);
        
        return newMode;
    }
    
    public bool DeleteCustomPerformanceMode(IPerformanceMode mode)
    {
        if (mode is not CustomPerformanceMode customPerformanceMode)
        {
            return false;
        }

        return AvailableModes.Remove(mode) && _config.CustomPerformanceModes.Remove(customPerformanceMode);
    }
    
    public IPerformanceMode ApplyModificationsFromCustomPerformanceMode(IPerformanceMode modeCopy)
    {
        if (modeCopy is not CustomPerformanceMode customPerformanceModeCopy)
        {
            return null;
        }

        var originalMode = _config.CustomPerformanceModes.FirstOrDefault(mode => mode.Id == customPerformanceModeCopy.Id);
        if (originalMode == null)
        {
            return null;
        }

        customPerformanceModeCopy.CopyTo(originalMode);
        
        var index = AvailableModes.IndexOf(originalMode);
        AvailableModes.Remove(originalMode);
        AvailableModes.Insert(index, originalMode);
        
        _config.SaveToLocalStorage();
        
        return originalMode;
    }
}