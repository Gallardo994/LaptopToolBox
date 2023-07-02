using System;
using GHelper.Injection;
using GHelper.Updates.Models;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Ninject;

namespace GHelper.Views
{
    public partial class UpdatesView
    {
        public UpdatesViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<UpdatesViewModel>();
        
        public UpdatesView()
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
                XamlRoot = XamlRoot,
            };
            
            var uri = new Uri(update.DownloadUrl);

            if (uri.AbsolutePath.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
            {
                contentDialog.PrimaryButtonText = "Open in Browser";
                contentDialog.SecondaryButtonText = "Download & Run";
            }

            var result = await contentDialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                await ViewModel.RequestDownloadUpdate(update);
            }
            else if (result == ContentDialogResult.Secondary)
            {
                await ViewModel.RequestDownloadAndInstallUpdate(uri);
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
