using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Ninject;

namespace GHelper.Pages
{
    public sealed partial class PerformancePage
    {
        public PerformanceViewModel PerformanceViewModel { get; } = Services.ResolutionRoot.Get<PerformanceViewModel>();
        
        public PerformancePage()
        {
            InitializeComponent();
            
            DataContext = PerformanceViewModel;
        }
    }
}
