using GHelper.Injection;
using GHelper.ViewModels;
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
    }
}
