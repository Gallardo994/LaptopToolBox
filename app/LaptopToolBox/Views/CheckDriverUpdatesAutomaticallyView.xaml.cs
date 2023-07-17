using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class CheckDriverUpdatesAutomaticallyView
    {
        public CheckDriverUpdatesAutomaticallyViewModel ViewModel { get; } = Services.ResolutionRoot.Get<CheckDriverUpdatesAutomaticallyViewModel>();
        
        public CheckDriverUpdatesAutomaticallyView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
