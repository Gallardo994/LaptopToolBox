using System;
using LaptopToolBox.AppWindows;
using LaptopToolBox.DeviceControls.PerformanceModes;
using LaptopToolBox.Pages;
using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class CpuMonitorView
    {
        public CpuMonitorViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<CpuMonitorViewModel>();
        
        public CpuMonitorView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
