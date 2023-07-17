using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class AppOutdatedWarningView
    {
        public AppOutdatedWarningViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<AppOutdatedWarningViewModel>();
        
        public AppOutdatedWarningView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
