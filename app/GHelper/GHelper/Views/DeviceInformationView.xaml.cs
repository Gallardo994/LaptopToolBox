using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
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
