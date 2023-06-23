using GHelper.Injection;
using GHelper.VendorServices;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class VendorServicesView
    {
        private readonly IVendorServicesControl _vendorServicesControl = Services.ResolutionRoot.Get<IVendorServicesControl>();
        public VendorServicesViewModel ViewModel { get; } = Services.ResolutionRoot.Get<VendorServicesViewModel>();
        
        public VendorServicesView()
        {
            InitializeComponent();
        }

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            _vendorServicesControl.Enable();
        }
        
        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            _vendorServicesControl.Disable();
        }
    }
}
