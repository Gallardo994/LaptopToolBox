using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class AuraView
    {
        public AuraViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<AuraViewModel>();
        
        public AuraView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
