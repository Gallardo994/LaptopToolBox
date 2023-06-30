using System;
using Windows.System;
using GHelper.About;
using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Ninject;

namespace GHelper.Pages
{
    public sealed partial class AboutPage
    {
        public AboutViewModel ViewModel { get; } = Services.ResolutionRoot.Get<AboutViewModel>();
        
        public AboutPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
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

        private async void LinkOpen_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.DataContext as IAboutItem;
            
            if (item == null)
            {
                return;
            }
            
            var contentDialog = new ContentDialog
            {
                Title = "Open Link",
                Content = $"Go to {item.Link}",
                PrimaryButtonText = "Yes",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonText = "No",
                XamlRoot = Content.XamlRoot,
            };

            var result = await contentDialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                await Launcher.LaunchUriAsync(new Uri(item.Link));
            }
        }

        private async void LicenseLinkOpen_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.DataContext as IAboutItem;
            
            if (item == null)
            {
                return;
            }
            
            var contentDialog = new ContentDialog
            {
                Title = "Open Link",
                Content = $"Go to {item.LicenseLink}",
                PrimaryButtonText = "Yes",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonText = "No",
                XamlRoot = Content.XamlRoot,
            };

            var result = await contentDialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                await Launcher.LaunchUriAsync(new Uri(item.LicenseLink));
            }
        }

        private void UpdateButton_Pressed(object sender, RoutedEventArgs e)
        {
            ViewModel.PerformUpdatesCheck();
        }

        private void InstallUpdateButton_Pressed(object sender, RoutedEventArgs e)
        {
            ViewModel.InstallUpdate();
        }
    }
}
