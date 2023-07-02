using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Ninject;

namespace GHelper.Views
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
