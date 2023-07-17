using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class DeviceOutdatedWarningView
    {
        public DeviceOutdatedWarningViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<DeviceOutdatedWarningViewModel>();
        
        public DeviceOutdatedWarningView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
