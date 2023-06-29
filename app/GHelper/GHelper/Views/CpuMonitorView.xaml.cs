using System;
using GHelper.AppWindows;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using GHelper.Pages;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Ninject;

namespace GHelper.Views
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
