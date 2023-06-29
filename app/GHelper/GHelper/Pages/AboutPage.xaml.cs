using System;
using System.Diagnostics;
using System.Threading;
using Windows.System;
using GHelper.About;
using GHelper.AppUpdater;
using GHelper.Helpers;
using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Ninject;
using Serilog;

namespace GHelper.Pages
{
    public sealed partial class AboutPage
    {
        private readonly IAppUpdater _appUpdater = Services.ResolutionRoot.Get<IAppUpdater>();
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
        
        private async void UpdateButton_Pressed(object sender, RoutedEventArgs e)
        {
            ViewModel.IsCheckingForUpdates = true;
            
            var release = await _appUpdater.GetSuggestedUpdate();
            
            if (release == null)
            {
                var dialogNoNewVersion = new ContentDialog
                {
                    XamlRoot = XamlRoot,
                    Title = "No Update Available",
                    Content = "No update is available at this time.",
                    CloseButtonText = "OK",
                };
                
                await dialogNoNewVersion.ShowAsync();

                ViewModel.IsCheckingForUpdates = false;
                return;
            }

            ViewModel.IsCheckingForUpdates = false;
            
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Update Available",
                Content = $"Version {release.Name} is available. Would you like to update?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                Log.Debug("User chose not to update");
                return;
            }
            
            var notificationDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Update Downloading",
                Content = "The update is downloading. You will be prompted to install it when it is finished.",
                CloseButtonText = "OK",
            };
            
            await notificationDialog.ShowAsync();

            ViewModel.IsDownloadingUpdate = true;

            var downloadCts = new CancellationTokenSource();
            var zipPath = await _appUpdater.Download(release, downloadCts.Token);
        
            if (string.IsNullOrEmpty(zipPath))
            {
                Log.Error("Failed to download release");
                ViewModel.IsDownloadingUpdate = false;
                return;
            }
        
            Log.Debug("Downloaded release to {zipPath}", zipPath);

            ViewModel.IsDownloadingUpdate = false;
            
            var installDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Update Downloaded",
                Content = "The update has finished downloading",
                CloseButtonText = "Install",
            };
            
            await installDialog.ShowAsync();
            
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "UpdateInstaller\\UpdateInstaller.exe",
                Arguments = $"--zipPath=\"{zipPath}\" --targetDir=\"{AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')}\" --runAfter=\"GHelper.exe\"",
                UseShellExecute = true,
            };
            
            Process.Start(processStartInfo);
            
            ApplicationHelper.Exit();
        }
    }
}
