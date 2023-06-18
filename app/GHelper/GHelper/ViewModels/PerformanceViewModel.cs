﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.DeviceControls;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class PerformanceViewModel : INotifyPropertyChanged
{
    private readonly IPerformanceModeControl _performanceModeControl;
    private readonly IPerformanceModesProvider _performanceModesProvider;
    private readonly IAcpi _acpi;
    
    public ObservableCollection<PerformanceMode> Modes => _performanceModesProvider.AvailableModes;
    public bool IsAvailable => _acpi.IsAvailable;

    private PerformanceMode _selectedMode;
    public PerformanceMode SelectedMode
    {
        get => _selectedMode;
        set
        {
            _selectedMode = value;
            SetPerformanceMode(value);
            OnPropertyChanged();
        }
    }
    
    public PerformanceViewModel()
    {
        _performanceModeControl = Services.ResolutionRoot.Get<IPerformanceModeControl>();
        _performanceModesProvider = Services.ResolutionRoot.Get<IPerformanceModesProvider>();
        _acpi = Services.ResolutionRoot.Get<IAcpi>();
    }
    
    public void SetPerformanceMode(PerformanceMode performanceMode)
    {
        _performanceModeControl.SetMode(performanceMode);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}