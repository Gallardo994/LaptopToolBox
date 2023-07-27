using System;
using System.Diagnostics;
using Windows.System;
using LaptopToolBox.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace LaptopToolBox.Pages
{
    public sealed partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void QuitButton_OnClick(object sender, RoutedEventArgs e)
        {
            var contentDialog = new ContentDialog
            {
                Title = "Quit",
                Content = "Are you sure you want to completely close Laptop ToolBox? \nYour device will not be controlled by Laptop ToolBox anymore.",
                PrimaryButtonText = "Yes",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonText = "No",
                XamlRoot = Content.XamlRoot,
            };
            
            var result = await contentDialog.ShowAsync();
            
            if (result != ContentDialogResult.Primary)
            {
                return;
            }
            
            ApplicationHelper.Exit();
        }

        private async void ReportIssue_OnClick(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/Gallardo994/LaptopToolBox/issues/new/choose"));
        }

        private void OpenLogsFolderButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", ApplicationHelper.AppDataFolder);
        }
    }
}
