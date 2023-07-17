using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class UnderVoltView
    {
        public UnderVoltViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<UnderVoltViewModel>();
        
        public UnderVoltView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void UnderVoltApply_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.UnderVolt = (int) UnderVoltSlider.Value;
        }
    }
}
