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
    public sealed partial class PerformanceModeView
    {
        private readonly MainWindow _mainWindow = Services.ResolutionRoot.Get<MainWindow>();
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
        
        private void ModifyPerformanceMode_OnClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            var performanceMode = (sender as Button)?.DataContext as CustomPerformanceMode;
            
            if (performanceMode == null)
            {
                return;
            }

            OpenPerformanceModeModification(performanceMode);
        }
        
        private void OpenPerformanceModeModification(CustomPerformanceMode performanceMode)
        {
            _mainWindow.AddThisPageToBackStack();
            _mainWindow.NavigateContentFrame(
                typeof(ModifyPerformanceProfilePage), 
                performanceMode.Id,
                new SlideNavigationTransitionInfo
                {
                    Effect = SlideNavigationTransitionEffect.FromRight
                },
                false);
        }
        
        private async void DeletePerformanceMode_OnClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            var performanceMode = (sender as Button)?.DataContext as CustomPerformanceMode;
            
            if (performanceMode == null)
            {
                return;
            }
            
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Delete Performance Profile",
                Content = $"Are you sure you want to delete the performance profile \"{performanceMode.Title}\"?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
            };
            
            var result = await dialog.ShowAsync();
            
            if (result != ContentDialogResult.Primary)
            {
                return;
            }
            
            ViewModel.DeletePerformanceMode(performanceMode);
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

        private async void ButtonAddPerformanceMode_OnClicked(object sender, RoutedEventArgs e)
        {
            var titleTextBox = new TextBox
            {
                PlaceholderText = "Title",
            };
            
            var descriptionTextBox = new TextBox
            {
                PlaceholderText = "Description (optional)",
                Margin = new Thickness(0, 10, 0, 0)
            };
            
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "New Performance Profile",
                PrimaryButtonText = "Create",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                Content = new StackPanel
                {
                    Children =
                    {
                        titleTextBox,
                        descriptionTextBox,
                    }
                }
            };

            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            var mode = ViewModel.AddCustomPerformanceMode(titleTextBox.Text, descriptionTextBox.Text);
            
            if (mode is not CustomPerformanceMode performanceMode)
            {
                return;
            }
            
            OpenPerformanceModeModification(performanceMode);
        }
    }
}
