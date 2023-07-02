using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
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
