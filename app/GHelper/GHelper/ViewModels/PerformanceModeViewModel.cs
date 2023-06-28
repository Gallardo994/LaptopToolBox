using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using Ninject;
using Serilog;

namespace GHelper.ViewModels;

public partial class PerformanceModeViewModel : ObservableObject
{
    private readonly IPerformanceModeControl _performanceModeControl;
    private readonly IPerformanceModesProvider _performanceModesProvider;
    private readonly IAcpi _acpi;
    
    public ObservableCollection<IPerformanceMode> Modes => _performanceModesProvider.AvailableModes;
    public bool IsAvailable => _acpi.IsAvailable;

    [ObservableProperty] private IPerformanceMode _selectedMode;

    public PerformanceModeViewModel()
    {
        _performanceModeControl = Services.ResolutionRoot.Get<IPerformanceModeControl>();
        _performanceModesProvider = Services.ResolutionRoot.Get<IPerformanceModesProvider>();
        _acpi = Services.ResolutionRoot.Get<IAcpi>();
        
        PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(SelectedMode))
            {
                SetPerformanceMode(SelectedMode);
            }
        };
    }
    
    private void SetPerformanceMode(IPerformanceMode performanceMode)
    {
        _performanceModeControl.SetMode(performanceMode);
    }
    
    public IPerformanceMode AddCustomPerformanceMode(string title, string description)
    {
        return _performanceModesProvider.CreateCustomPerformanceMode(title, description);
    }
    
    public void DeletePerformanceMode(IPerformanceMode performanceMode)
    {
        var currentMode = _performanceModeControl.GetCurrentMode();
        
        if (currentMode == performanceMode)
        {
            _performanceModeControl.RestoreToFallbackMode();
        }
        
        _performanceModesProvider.DeleteCustomPerformanceMode(performanceMode);
    }
}