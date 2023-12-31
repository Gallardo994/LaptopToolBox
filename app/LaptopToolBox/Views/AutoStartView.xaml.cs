using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class AutoStartView
    {
        public AutoStartViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<AutoStartViewModel>();
        
        public AutoStartView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            ViewModel.IsAutoStartEnabled = ((ToggleSwitch) sender).IsOn;
        }
    }
}
