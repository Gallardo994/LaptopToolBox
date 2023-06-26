using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class AutoOverdriveView
    {
        public AutoOverdriveViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<AutoOverdriveViewModel>();
        
        public AutoOverdriveView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
