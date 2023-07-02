using System;
using GHelper.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper.Pages
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
                Content = "Are you sure you want to completely close GHelper? \nYour device will not be controlled by GHelper anymore.",
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
    }
}
