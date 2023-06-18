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

        private void SetPerformanceMode_OnClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            PerformanceViewModel.SelectedMode = (IPerformanceMode) (sender as Button)?.DataContext;
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
