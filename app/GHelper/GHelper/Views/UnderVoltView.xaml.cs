using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Ninject;

namespace GHelper.Views
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
