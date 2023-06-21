using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class PerformanceModeView
    {
        public PerformanceModeViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<PerformanceModeViewModel>();
        
        public PerformanceModeView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void SetPerformanceMode_OnClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            ViewModel.SelectedMode = (IPerformanceMode) (sender as Button)?.DataContext;
        }
        
        private void ListViewSwipeContainer_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Microsoft.UI.Input.PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == Microsoft.UI.Input.PointerDeviceType.Pen)
            {
                VisualStateManager.GoToState(sender as Control, "HoverButtonsShown", true);
            }
        }

        private void ListViewSwipeContainer_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as Control, "HoverButtonsHidden", true);
        }

        private void ButtonAddPerformanceMode_OnClicked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
