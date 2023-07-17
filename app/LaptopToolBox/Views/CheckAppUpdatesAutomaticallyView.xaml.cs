using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class CheckAppUpdatesAutomaticallyView
    {
        public CheckAppUpdatesAutomaticallyViewModel ViewModel { get; } = Services.ResolutionRoot.Get<CheckAppUpdatesAutomaticallyViewModel>();
        
        public CheckAppUpdatesAutomaticallyView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
