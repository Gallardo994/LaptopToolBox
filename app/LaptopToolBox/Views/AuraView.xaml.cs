using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
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
