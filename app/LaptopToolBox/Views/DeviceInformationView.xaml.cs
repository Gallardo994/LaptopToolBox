using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class DeviceInformationView
    {
        public DeviceInformationViewModel ViewModel { get; } = Services.ResolutionRoot.Get<DeviceInformationViewModel>();
        
        public DeviceInformationView()
        {
            InitializeComponent();
        }
    }
}
