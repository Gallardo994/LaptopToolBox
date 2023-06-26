using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class FansView
    {
        public FansViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<FansViewModel>();
        
        public FansView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
