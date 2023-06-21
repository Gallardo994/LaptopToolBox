using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.DeviceControls.Acpi;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class PerformanceModeViewModel
{
    private readonly IPerformanceModeControl _performanceModeControl;
    private readonly IPerformanceModesProvider _performanceModesProvider;
    private readonly IAcpi _acpi;
    
    public ObservableCollection<IPerformanceMode> Modes => _performanceModesProvider.AvailableModes;
    public bool IsAvailable => _acpi.IsAvailable;

    private IPerformanceMode _selectedMode;
    public IPerformanceMode SelectedMode
    {
        get => _selectedMode;
        set
        {
            _selectedMode = value;
            SetPerformanceMode(value);
            OnPropertyChanged();
        }
    }
    
    public PerformanceModeViewModel()
    {
        _performanceModeControl = Services.ResolutionRoot.Get<IPerformanceModeControl>();
        _performanceModesProvider = Services.ResolutionRoot.Get<IPerformanceModesProvider>();
        _acpi = Services.ResolutionRoot.Get<IAcpi>();
    }
    
    public void SetPerformanceMode(IPerformanceMode performanceMode)
    {
        _performanceModeControl.SetMode(performanceMode);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}