using GHelper.Helpers;
using Microsoft.UI.Xaml;

namespace GHelper.Pages
{
    public sealed partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void QuitButton_OnClick(object sender, RoutedEventArgs e)
        {
            ApplicationHelper.Exit();
        }
    }
}
