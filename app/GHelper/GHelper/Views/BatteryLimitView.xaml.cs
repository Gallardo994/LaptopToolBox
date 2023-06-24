using GHelper.Configs;
using GHelper.DeviceControls.Battery;
using GHelper.Injection;
using GHelper.VendorServices;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class BatteryLimitView
    {
        private readonly IConfig _config = Services.ResolutionRoot.Get<IConfig>();
        private readonly IBattery _battery = Services.ResolutionRoot.Get<IBattery>();
        public BatteryLimitViewModel ViewModel { get; } = Services.ResolutionRoot.Get<BatteryLimitViewModel>();
        
        public BatteryLimitView()
        {
            InitializeComponent();
            
            LimitComboBox.Text = _config.BatteryLimit.ToString();
            LimitComboBox.SelectedValue = LimitComboBox.Text;
            LimitComboBox.PlaceholderText = LimitComboBox.Text;
        }

        private void BatteryLimit_Submitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
        {
            if (int.TryParse(args.Text, out var limit))
            {
                _battery.SetBatteryLimit(limit);
                _config.BatteryLimit = limit;
            }
        }
    }
}
