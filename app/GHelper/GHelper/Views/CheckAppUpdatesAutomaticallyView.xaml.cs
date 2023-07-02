using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
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
