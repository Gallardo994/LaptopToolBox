using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class AutoEcoView
    {
        public AutoEcoViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<AutoEcoViewModel>();
        
        public AutoEcoView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
