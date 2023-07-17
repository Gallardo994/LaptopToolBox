using System;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.Commands;
using LaptopToolBox.Helpers;
using LaptopToolBox.Injection;
using LaptopToolBox.VendorServices;
using Ninject;

namespace LaptopToolBox.ViewModels;

public partial class VendorServicesViewModel : ObservableObject, IDisposable
{
    private readonly ISTACommandLoop _commandLoop = Services.ResolutionRoot.Get<ISTACommandLoop>();
    private readonly IVendorServicesControl _vendorServicesControl = Services.ResolutionRoot.Get<IVendorServicesControl>();
    
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HeaderTextWithCount))] private int _countRunning;
    public string HeaderTextWithCount => $"Vendor Services (Active: {CountRunning})";

    private SafeTimer _timer = new(500);
    
    public VendorServicesViewModel()
    {
        _countRunning = _vendorServicesControl.CountRunningSlow();
        
        _timer.Elapsed += (sender, args) =>
        {
            _commandLoop.Enqueue(() =>
            {
                CountRunning = _vendorServicesControl.CountRunningSlow();
            });
        };
        
        _timer.Start();
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _timer = null;
    }
}