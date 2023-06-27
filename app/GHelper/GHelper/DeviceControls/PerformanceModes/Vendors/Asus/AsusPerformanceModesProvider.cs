using System;
using System.Collections.ObjectModel;
using System.Linq;
using GHelper.Configs;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.PerformanceModes.Vendors.Asus;

public class AsusPerformanceModesProvider : IPerformanceModesProvider
{
    private readonly IConfig _config;
    
    public ObservableCollection<IPerformanceMode> AvailableModes { get; init; }

    [Inject]
    public AsusPerformanceModesProvider(IConfig config)
    {
        _config = config;
        
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
        
        Log.Debug("Number of custom performance modes: {Count}", _config.CustomPerformanceModes.Count);
        foreach (var customPerformanceMode in _config.CustomPerformanceModes)
        {
            Log.Debug("Type of custom performance mode: {Type}", customPerformanceMode.GetType());
            AvailableModes.Add(customPerformanceMode);
        }

        var testGuid = Guid.Parse("479071d6-8f6b-4709-9a4d-3460f529f0a7"); // Test guid
        
        if (AvailableModes.All(mode => mode.Id != testGuid))
        {
            var newMode = new CustomPerformanceMode
            {
                Id = testGuid,
                Title = "Test",
                Description = "Test",
            };
            
            AvailableModes.Add(newMode);
            _config.CustomPerformanceModes.Add(newMode);
        }
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
    
    public IPerformanceMode CreateCustomPerformanceMode(string title, string description = "")
    {
        var newMode = new CustomPerformanceMode
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
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
}