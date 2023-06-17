using GHelper.DeviceControls.Aura;
using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class AuraView
    {
        public IAuraViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<IAuraViewModel>();
        
        public AuraView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Mode = ((AuraModeModel) ((ComboBox) sender).SelectedItem).Mode;
        }
    }
}
