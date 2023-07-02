using System;
using System.Threading.Tasks;
using Windows.System;
using GHelper.Commands;
using GHelper.Helpers;
using GHelper.Injection;
using GHelper.Updates.Core;
using GHelper.Updates.Models;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Ninject;

namespace GHelper.Pages
{
    public sealed partial class UpdatesPage
    {
        public UpdatesViewModel ViewModel { get; } = Services.ResolutionRoot.Get<UpdatesViewModel>();
        
        public UpdatesPage()
        {
            InitializeComponent();
        
            DataContext = ViewModel;
        }
        
        private void Button_OnClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            RefreshUpdates();
        }
    
        private async void DownloadButton_OnClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            var button = sender as Button;
            var update = button?.DataContext as IUpdate;
        
            if (update == null)
            {
                return;
            }

            var contentDialog = new ContentDialog
            {
                Title = "Download",
                Content = $"Go to {update.DownloadUrl}",
                PrimaryButtonText = "Yes",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonText = "No",
                XamlRoot = Content.XamlRoot,
            };

            var result = await contentDialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                await Launcher.LaunchUriAsync(new Uri(update.DownloadUrl));
            }
        }

        private void RefreshUpdates()
        {
            ViewModel.CheckForUpdates();
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
    }
}
