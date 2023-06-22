using CommunityToolkit.Mvvm.Input;
using GHelper.AppWindows;
using GHelper.Injection;
using GHelper.ViewModels;
using H.NotifyIcon;
using Microsoft.UI.Xaml;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class TrayView
    {
        public TrayViewModel ViewModel { get; } = Services.ResolutionRoot.Get<TrayViewModel>();
        
        public TrayView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
        
        [RelayCommand]
        private void ShowHideWindow()
        {
            var mainWindow = Services.ResolutionRoot.Get<MainWindow>();
            
            if (mainWindow.Visible)
            {
                mainWindow.Hide();
            }
            else
            {
                mainWindow.Show();
            }
        }
        
        [RelayCommand]
        private void ExitApplication()
        {
            TrayIcon.Dispose();
            Application.Current.Exit();
        }
    }
}
