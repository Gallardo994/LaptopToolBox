using System;
using System.Threading.Tasks;
using Windows.System;
using GHelper.Injection;
using GHelper.Updates.Core;
using GHelper.Updates.Models;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper.Pages
{
    public sealed partial class UpdatesPage
    {
        private readonly IUpdatesChecker _updatesChecker = Services.ResolutionRoot.Get<IUpdatesChecker>();
        public IUpdatesViewModel ViewModel { get; } = Services.ResolutionRoot.Get<IUpdatesViewModel>();

        public UpdatesPage()
        {
            InitializeComponent();
        
            DataContext = ViewModel;
        
            RefreshUpdates();
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
            ViewModel.IsUpdating = true;
            Task.Run(async () =>
            {
                var result = await _updatesChecker.CheckForUpdates();
                DispatcherQueue.TryEnqueue(() =>
                {
                    ViewModel.SetUpdates(result);
                    ViewModel.IsUpdating = false;
                });
            }).Forget();
        }
    }
}
