using CommunityToolkit.Mvvm.Input;
using GHelper.AppWindows;
using GHelper.Controllers;
using GHelper.Helpers;
using GHelper.Injection;
using GHelper.ViewModels;
using H.NotifyIcon;
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
            var controller = Services.ResolutionRoot.Get<IMainWindowController>();
            controller.ToggleState();
        }
        
        [RelayCommand]
        private void ExitApplication()
        {
            ApplicationHelper.Exit();
        }
    }
}
